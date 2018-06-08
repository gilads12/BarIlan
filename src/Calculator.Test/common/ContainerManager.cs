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

    public static class ContainerManager
    {
        public static void DockerComposePull(this DockerSettings settings)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments =
                $"-f {settings.DockerComposePath} pull"
            };

            AddEnvironmentVariables(processInfo, settings);
            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }
        public static string GetDockerMachineIp()
        {
            try
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
            catch { return "127.0.0.1"; }
        }
        public static void DockerComposeBuild(this DockerSettings settings)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments =
                $"-f { settings.DockerComposePath} build"
            };

            AddEnvironmentVariables(processInfo, settings);
            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }
        public static void DockerComposeUp(this DockerSettings settings)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = $"-f {settings.DockerComposePath} up -d"
            };

            AddEnvironmentVariables(processInfo, settings);
            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
            var up = WaitForDockerUp(settings.Url).Result;

            if (!up)
            {
                throw new Exception($"Startup failed, could not get '{settings.Ip}:{settings.ExternalPort}'");
            }

        }
        public static void DockerComposeDown(this DockerSettings settings)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = $"-f {settings.DockerComposePath} down --rmi local"
            };
            AddEnvironmentVariables(processInfo, settings);

            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.ExitCode.Should().Be(0);
        }


        private static async Task<bool> WaitForDockerUp(string url)
        {
            using (var client = new WebClient())
            {
                var result = Policy.Handle<Exception>().
                           WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                           .ExecuteAsync(async () =>
                           {
                               string HTMLSource = await client.DownloadStringTaskAsync(new Uri(url));
                               return true;
                           })
                           .GetAwaiter()
                           .GetResult();
                return result;
            }
        }
        private static void AddEnvironmentVariables(ProcessStartInfo processStartInfo, DockerSettings variables)
        {
            processStartInfo.Environment["TAG"] = variables.Tag;
            processStartInfo.Environment["COMPUTERNAME"] = variables.MachineName;
            processStartInfo.Environment["ExternalPort"] = variables.ExternalPort;

        }




    }

}
