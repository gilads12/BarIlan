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
                CalculatorState = string.Empty,
                Input="1"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);//tbc

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
                CalculatorState = string.Empty,
                Input = "+"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);//tbc

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
                CalculatorState = string.Empty,
                Input = "9+"
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);//tbc

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
                CalculatorState =@"3+7/2+6",
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            // Act
            var response = await _client.PostAsync("/calculate", stringContent);//tbc

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
                CalculatorState = @"3+8/2+6",
                Input = "="
            };
            var content = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/calculate", stringContent);//tbc

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("11.5");
        }

    }
}
