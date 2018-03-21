using Calculator.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Test.UnitTesting
{
    [TestClass]

    public class CalculatorTest
    {
        [TestMethod]
        public void TestReturnsNullFromEmptyJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest();

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(default(string));
            response.CalculatorState.Should().Be(default(string));
        }

        [TestMethod]
        public void TestReturnsInputFromEmptyJsonRequestState()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5" };
            string result = "5";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestReturnsInputFromNotEmptyJsonRequestState()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5", CalculatorState = "1" };
            string result = "15";

            //act
            JsonResponse response = request.CalculateNextState();// we call the GetLastNumeric can create stub but unneccery...

            //assert
            response.Display.Should().Be(result);
        }
    }
}
