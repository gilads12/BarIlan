using Calculator.Core;
using Calculator.WebApi.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class CalculatorControllerTest
    {

        [TestMethod]
        public void TestAsync()
        {
            //arrange
            var controller = new CalculatorController(null);
            var request = new JsonRequest { CalculatorState = "1+4", Input = "=" };
            string restul = "5";

            //act
            var result = controller.Calculate(request);


            //assert
            result.Display.Should().Be(restul);
        }
    }
}
