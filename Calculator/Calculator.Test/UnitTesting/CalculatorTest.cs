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

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            Assert.AreEqual(response.Display, default(string));
            Assert.AreEqual(response.CalculatorState, default(string));
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
            Assert.AreEqual(result, response.Display);
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
            Assert.AreEqual(result, response.Display);
        }
    }
}
