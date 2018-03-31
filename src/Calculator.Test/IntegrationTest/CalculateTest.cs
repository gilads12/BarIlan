using Calculator.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test.IntegrationTest
{
    [TestClass]
    public class CalculateTest
    {
        [TestMethod]
        public void TestSumCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = "15+4" };
            string result = "19";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestComplexCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = @"15+6-2" };
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
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = @"15+6*2-5" };
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
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = @"15+6*2-5-2/5" };
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
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = @"15+6*2-5-2/5=3+5" };
            string result = "8";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestUnfinshedCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "+", CalculatorState = @"15+6*2-5-2/5=3+5" };
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
            JsonRequest request = new JsonRequest { Input = "=", CalculatorState = @"15+6*2-5-2/5=3-15" };
            string result = "-12";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

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
