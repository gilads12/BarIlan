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
            JsonRequest request = new JsonRequest { Input = "=", calculatorState = new JsonResponse { State = "15+4" } };
            string result = "19";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestFloatDivCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", calculatorState=new JsonResponse { State = "10/3" } };
            string result = (10/3f).ToString();

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestComplexMultCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", calculatorState=new JsonResponse { State = @"15+6*2-5" } };
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
            JsonRequest request = new JsonRequest { Input = "=", calculatorState=new JsonResponse { State = @"15+6*2-5-2/5" } };
            string result = "7";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestComplexCalculationWithNegativeNumbersFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", calculatorState = new JsonResponse { State = "15+4--7+-8*-4" } };
            string result = "-72";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestMultiAssigmentsCalculationFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", calculatorState= new JsonResponse { State = @"15+6*2-5-2/5=+3+5" } };
            string result = "15";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestCalculationWithNagativeNumbersFromJsonRequest()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "=", calculatorState=new JsonResponse { State = @"15+6*2-5-2/5-3-15" } };
            string result = "-11";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }

        [TestMethod]
        public void TestEmptyJsonRequestReturnEmptyDisplay()
        {
            //arrange 
            JsonRequest request = new JsonRequest();

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be("");
        }
       
        [TestMethod]
        public void TestReturnsInputFromNotEmptyJsonRequestState()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5", calculatorState=new JsonResponse { State = "1",Display="1" } };
            string result = "15";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
        [TestMethod]
        public void TestNegativeDisplay()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "8", calculatorState = new JsonResponse { State = @"34/-17",Display="-17" } };
            string result = "-178";

            //act
            JsonResponse response = request.CalculateNextState();

            //assert
            response.Display.Should().Be(result);
        }
    }
}
