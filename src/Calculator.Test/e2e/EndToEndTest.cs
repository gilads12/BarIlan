using Calculator.Core;
using FluentAssertions;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Test.e2e
{
    [TestClass]
    public class EndToEndTest
    {
        private readonly HttpClient _client = new HttpClient();
        private string _ip;
        private string _uri;
        private readonly int _port = 5001; //todo fix hard coded!!
        private readonly string _dockerPath = @"C:\Users\gilad\BarIlan\src\Calculator.Test\e2e\docker-compose.yml";//todo fix relative path 
        private Process _process;

        [TestInitialize]
        public void SetUp()
        {
            this._ip = GetDockerMachineIp();
            var processInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/C docker-compose -f " + this._dockerPath + " up ",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            this._process = Process.Start(processInfo);

            this._uri = "http://" + this._ip + ":" + this._port + "/calculate";
        }
        [TestCleanup]
        public void Dispose()
        {
            this._client.Dispose();
            this._process.Close();
        }


        [TestMethod]
        public async Task TestAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState=new JsonResponse { },
                Input = "1"
            };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("1");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "2";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("12");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "+";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("12");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("15");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "/";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("15");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "10";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("10");
                //--------------//

                //--------------//
                request.calculatorState.State = response.State;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-5");
                //--------------//
                
            }

            catch
            {
                throw;
            }
        }


        private async Task<JsonResponse> SendJsonRequestAsync(JsonRequest request)
        {
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(this._uri, stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonResponse>(responseString);
        }
        private string GetDockerMachineIp()
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/C docker-machine ip",
                UseShellExecute = false,
                RedirectStandardError = false,
                RedirectStandardOutput = true
            };

            var proc = Process.Start(processInfo);
            proc.WaitForExit();
            string line = proc.StandardOutput.ReadToEnd();

            proc.Close();
            return line.Replace("\n", "");
        }
    }
}
