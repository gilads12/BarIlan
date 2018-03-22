using Calculator.Core;
using Calculator.WebApi.Controllers;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class CalculatorControllerTest
    {

        [TestMethod]
        public void TestjsonRequestFromController()// todo rename 
        {
            //arrange
            var controller = new CalculatorController(new NullLogger<CalculatorController>());
            var request = new JsonRequest { CalculatorState = "1+4", Input = "=" };
            string restul = "5";

            //act
            var result = controller.Calculate(request);


            //assert
            result.Display.Should().Be(restul);
        }
    }
}
