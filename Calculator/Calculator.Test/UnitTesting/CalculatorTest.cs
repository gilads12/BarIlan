using Calculator.Core;
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
            Core.Calculator calculator = new Core.Calculator();

            //act
            JsonResponse response = calculator.CalculateNextState(request);

            //assert
            Assert.AreEqual(response.Display, default(string));
            Assert.AreEqual(response.CalculatorState, default(string));
        }

        [TestMethod]
        public void TestReturnsInputFromEmptyJsonRequestState()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5" };
            Core.Calculator calculator = new Core.Calculator();
            string result = "5";

            //act
            JsonResponse response = calculator.CalculateNextState(request);

            //assert
            Assert.AreEqual(result, response.Display);
        }

        [TestMethod]
        public void TestReturnsInputFromNotEmptyJsonRequestState()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5", CalculatorState = "1" };
            Core.Calculator calculator = new Core.Calculator();
            string result = "15";

            //act
            JsonResponse response = calculator.CalculateNextState(request);// we call the GetLastNumeric can create stub but unneccery...

            //assert
            Assert.AreEqual(result, response.Display);
        }
    }
}
