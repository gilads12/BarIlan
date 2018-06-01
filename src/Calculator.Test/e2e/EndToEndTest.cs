using Calculator.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Test.e2e
{
    [TestClass]
    public class EndToEndTest : IDisposable
    {
        private readonly HttpClient _client = new HttpClient();
        private ContainerManager _container;
        private string _uri;

        [TestInitialize]
        public void SetUp()
        {
            _container = new ContainerManager(@"../../../e2e/docker-compose.yml", "5001","test");
            _container.Init();
            this._uri = _container.Url + "/calculate";
        }

        [TestMethod]
        public void TestWithRetry()
        {
            Policy.Handle<Exception>().
                           WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                           .ExecuteAsync(TestAsync)
                           .GetAwaiter()
                           .GetResult();
        }

        private async Task TestAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
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
                request.calculatorState = response;
                request.Input = "2";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("12");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "+";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("12");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("15");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "/";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("15");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "10";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("10");
                //--------------//

                //--------------//
                request.calculatorState = response;
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

        public void Dispose()
        {
            this._client.Dispose();
            this._container.Dispose();
        }
    }

}
