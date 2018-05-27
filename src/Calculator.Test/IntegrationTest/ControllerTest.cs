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
            var request = new JsonRequest
            {
                Input = "1"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("1");
        }
        [TestMethod]
        public async Task TestEmptyStateOperatorInputRteutnInputAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = string.Empty },
                Input = "+"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be(default(string));
        }
        [TestMethod]
        public async Task TestDoubleInputRteutnInputAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = string.Empty },
                Input = "9+"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be(default(string));
        }
        [TestMethod]
        public async Task TestComplexSumAndMultRteurnResoultAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = @"3+7/2+6" },
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("11");
        }
        [TestMethod]
        public async Task TestComplexSumAndMultRteurnFloatResoultAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = @"3+8/2+6" },
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("11.5");
        }
        [TestMethod]
        public async Task TestNegativeParameterAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState=new JsonResponse {State= @"-3+8/5+6--1" },
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("8");
        }
        [TestMethod]
        public async Task TestNegativeResultAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState= new JsonResponse { State= @"2+8/5-6" },
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("-4");
        }
        [TestMethod]
        public async Task TestComplexSumAsync()// todo rename!
        {
            // Arrange
            var request = new JsonRequest
            {
                calculatorState = new JsonResponse { State = @"3+6=+3" },
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("12");
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
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().BeNull();//todo: does it realy should be null?
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
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().BeNull();//todo: does it realy should be null?
        }
        [TestMethod]
        public async Task TestCalculateEmptyStateAsync()
        {
            // Arrange
            var request = new JsonRequest
            {
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().BeNull();//todo: does it realy should be null?
        }
    }
}
