using Calculator.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Core.Exceptions;

namespace Calculator.Test.IntegrationTest
{
    [TestClass]
    public class CalculateTest
    {
        [TestMethod]
        public void TestSumCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = "15+4" };
            string result = "19";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestFloatSumCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = "15.2+4.5" };
            string result = "19.7";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestFloatDivCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = "10/3" };
            string result = (10/3f).ToString();

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestComplexCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = @"15+6-2" };
            string result = "19";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestComplexMultCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = @"15+6*2-5" };
            string result = "37";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestAllOperatorsCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = @"15+6*2-5-2/5" };
            string result = "7";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestMultiAssigmentsCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = @"15+6*2-5-2/5=+3+5" };
            string result = "15";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestUnfinshedCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "+", State = @"15+6*2-5-2/5=3+5" };
            string result = "5";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestCalculationWithNagativeNumbersFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", State = @"15+6*2-5-2/5=-3-15" };
            string result = "-11";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidInput))]
        public void TestReturnsNullFromEmptyJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest();

            //act
            JsonResponse response = request.CalculateNextState();
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
            JsonRequest request = new JsonRequest { Input = "5", State = "1" };
            string result = "15";

            //act
            JsonResponse response = request.CalculateNextState();// we call the GetLastNumeric can create stub but unneccery...

            //assert
            response.Display.Should().Be(result);
        }





    }
}
