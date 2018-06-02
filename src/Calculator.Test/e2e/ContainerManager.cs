using FluentAssertions;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Calculator.Test.e2e
{
    // we didn't found any framework in c# that handle docker-compose so we write this litle manager to run our end-to-end test
    // the build method will build the docker image and give the image a tag so this method will take a while (abuot a minute)
    // when we dispose this object we remove the image we create beacuse we want "clean setup" for each test
    // because of that each time we run this test will take abuot a minute (include polly) 
    // we didn't found out how to get the port from "docker-compose up" so we give static port!

    public class ContainerManager : IDisposable
    {
        private readonly string _port;
        private readonly string _dockerComposeFullPath;
        private readonly string _tag;
        private string _machineIp;


        public string Url { get; private set; }

        public ContainerManager(string dockerComposeFullPath, string port, string tag)
        {
            this._dockerComposeFullPath = dockerComposeFullPath;
            this._port = port;
            this._tag = tag;
        }
        public void Init()
        {
            this._machineIp = GetDockerMachineIp();
            BuildDockerCompose();
            DockerComposeUp();
            this.Url = "http://" + this._machineIp + ":" + this._port;

            var started = WaitForImage().Result;

            if (!started)
            {
                throw new Exception($"Startup failed, could not get '{this.Url}'");
            }
        }

        private async Task<bool> WaitForImage()
        {
            using (var client = new WebClient())
            {
                var result = Policy.Handle<Exception>().
                           WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                           .ExecuteAsync(async () =>
                           {
                               string HTMLSource = await client.DownloadStringTaskAsync(new Uri(this.Url));
                               return true;
                           })
                           .GetAwaiter()
                           .GetResult();
                return result;
            }
        }
        private string GetDockerMachineIp()
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-machine",
                Arguments = "ip",
                RedirectStandardOutput = true
            };

            var proc = Process.Start(processInfo);
            proc.WaitForExit();
            proc.ExitCode.Should().Be(0);
            string line = proc.StandardOutput.ReadToEnd();

            return line.Replace("\n", "");
        }
        private void BuildDockerCompose()
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments =
                $"-f {this._dockerComposeFullPath} build"
            };

            AddEnvironmentVariables(processInfo);
            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }
        private void DockerComposeUp()
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = $"-f {this._dockerComposeFullPath} up -d"
            };
            AddEnvironmentVariables(processInfo);
            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }
        private void DockerComposeRemove()
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = $"-f {this._dockerComposeFullPath} down --rmi local"
            };
            AddEnvironmentVariables(processInfo);

            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }
        private void AddEnvironmentVariables(ProcessStartInfo processStartInfo)
        {
            processStartInfo.Environment["TAG"] = this._tag;
            processStartInfo.Environment["COMPUTERNAME"] = Environment.MachineName;
        }

        public void Dispose()
        {
            DockerComposeRemove();
        }
    }

}
