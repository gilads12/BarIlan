using Calculator.Core;
using Calculator.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task TestEmptyStateRteutnInputAsync()
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
            var response = await _client.PostAsync("/api/Calculator/Calculate", stringContent);//tbc

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(responseString);
            jsonResponse.Display.Should().Be("1");
        }
    }
}
