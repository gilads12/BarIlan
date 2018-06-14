using Calculator.Core;
using Calculator.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Test.IntegrationTest
{
    [TestClass]
    public class ControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ControllerTest()
        {
            this._server = new TestServer(new WebHostBuilder()
                                     .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public async Task TestEmptyStateNumericInputRteutnInputAsync()
        {
            // Arrange
            var request = new JsonRequest { Input = "1" };

            //act
            JsonResponse response = await SendJsonRequestAsync(request);

            //assert
            response.Display.Should().Be("1");
        }
        [TestMethod]
        public async Task TestEmptyStateOperatorInputRteutnInputAsync()
        {
            // Arrange
            var request = new JsonRequest { Input = "+" };

            // Act
            JsonResponse response = await SendJsonRequestAsync(request);

            // Assert
            response.Display.Should().Be("");
        }
        [TestMethod]
        public async Task TestDoubleInputRteutnInputAsync()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "9+";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//
                
            }
            catch { throw; }
        }
        [TestMethod]
        public async Task TestNumberfterAssignAsync()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "+";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("3");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "6";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("6");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("9");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "3";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("93");
                //--------------//

            }
            catch { throw; }
       
        }

        [TestMethod]
        public async Task TestInvalidOperatorInputAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = @"2+8+5-6" },
                Input = ")"
            };

            // Act
            JsonResponse response = await SendJsonRequestAsync(request);

            // Assert
            response.Display.Should().Be("");
        }
        [TestMethod]
        public async Task TestCalculateInvalidOperatorStateAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = @"2+8+5)6" },
                Input = "="
            };

            // Act
            JsonResponse response = await SendJsonRequestAsync(request);

            // Assert
            response.Display.Should().Be("");
        }


        [TestMethod]
        public async Task TestCalculateEmptyStateAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                Input = "="
            };

            // Act
            JsonResponse response = await SendJsonRequestAsync(request);

            // Assert
            response.Display.Should().Be("");
        }
        [TestMethod]
        public async Task TestStartWithMinus()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "1";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-1");
                //--------------//
            }

            catch
            {
                throw;
            }
        }

        [TestMethod]
        public async Task TestDoubleOperators()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "+";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//
            }

            catch
            {
                throw;
            }
        }

        [TestMethod]
        public async Task TestMinusOperator()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "1";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("1");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("1");
                //--------------//
            }

            catch
            {
                throw;
            }
        }

        [TestMethod]
        public async Task TestMultNegativNumber()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "7";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("7");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "*";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("7");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "7";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-7");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-49");
                //--------------//
            }

            catch
            {
                throw;
            }
        }

        [TestMethod]
        public async Task TestNoOperatorAssigment()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "1";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("1");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("1");
                //--------------//
                
            }

            catch
            {
                throw;
            }
        }

        [TestMethod]
        public async Task TestNegativeNumerAfterAssigment()
        {
            // Arrange
            var request = new JsonRequest { };
            JsonResponse response;

            //testing
            //--------------//
            try
            {
                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "-";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "7";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-7");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "=";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-7");
                //--------------//

                //--------------//
                request.calculatorState = response;
                request.Input = "7";

                response = await SendJsonRequestAsync(request);
                response.Display.Should().Be("-77");
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

            var response = await _client.PostAsync("/calculate", stringContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonResponse>(responseString);
        }

    }
}
