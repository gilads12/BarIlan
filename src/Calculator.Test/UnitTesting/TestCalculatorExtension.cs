using Calculator.Core;
using Calculator.Core.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class TestCalculatorExtension
    {
        [TestMethod]
        public void TestSplitAndKeep()
        {
            //arrange 
            string str = "testsplitandkeep";
            List<string> expected = new List<string> { "t", "es", "t", "spli", "t", "andkeep" };

            //act
            List<string> result = str.SplitAndKeep(new char[] { '=', 't' }).ToList();

            //assert
            result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());

        }
        [TestMethod]
        public void TestInfixToPostfix()
        {
            //arrange //example of 1+2/5
            List<Token> infixTokens = new List<Token> { new NumericToken(1), new OperatorToken('+'), new NumericToken(2), new OperatorToken('/'), new NumericToken(3) };
            List<string> expected = new List<string> { "1", "2", "+", "3", "/" };

            //act
            List<Token> result = infixTokens.InfixToPostfix().ToList();
            List<string> resultString = new List<string>();
            foreach (var i in result)
            {
                switch (i)
                {
                    case NumericToken nt:
                        resultString.Add(nt.value.ToString());
                        break;
                    case OperatorToken ot:
                        resultString.Add(ot.value.ToString());
                        break;
                }
            }

            //assert
            resultString.Should().BeEquivalentTo(expected,
                options => options.WithStrictOrdering());
        }
        [TestMethod]
        public void TestInsertMinusToToken()
        {
            //arrange //example of 1+2/5
            List<Token> infixTokens = new List<Token> { new NumericToken(1), new OperatorToken('+'),new OperatorToken('-'), new NumericToken(2), new OperatorToken('/'),
                new OperatorToken('-'), new NumericToken(3),new OperatorToken('+'), new OperatorToken('-'),new NumericToken(1),  new OperatorToken('-'), new OperatorToken('-'),
                new NumericToken(7), new OperatorToken('-'), new NumericToken(9)  };
            List<string> expected = new List<string> { "1", "+", "-2", "/", "-3", "+", "-1", "-", "-7", "-", "9" };

            //act
            List<Token> result = infixTokens.InsertMinusToToken().ToList();
            List<string> resultString = new List<string>();
            foreach (var i in result)
            {
                switch (i)
                {
                    case NumericToken nt:
                        resultString.Add(nt.value.ToString());
                        break;
                    case OperatorToken ot:
                        resultString.Add(ot.value.ToString());
                        break;
                }
            }

            //assert
            resultString.Should().BeEquivalentTo(expected,
                options => options.WithStrictOrdering());

        }
        [TestMethod]
        public void TestReturnsNegativeNumericFromFloatNumericString()
        {
            //arrange 
            string numeric = "-0.5";

            //act
            Token result = numeric.ToToken();

            //assert
            result.Should().BeOfType(typeof(NumericToken));
        }
        [TestMethod]
        [ExpectedException(typeof(NotValidTokenException))]
        public void TesteExceptuionNotvalidToken()
        {
            //arrange 
            string numeric = "(";

            //act
            Token result = numeric.ToToken();
        }
        [TestMethod]
        public void TestIsFloatNumberMinus()
        {
            //arrange 
            string str = "-";

            //act
            bool resoult = str.IsFloatNumber();

            //assert
            resoult.Should().Be(false);
        }
        [TestMethod]
        public void TestGetStateEmptyRequst()
        {
            //arrange 
            JsonRequest request = new JsonRequest();

            //act
            string result = request.GetState();

            //assert
            result.Should().Be("");
        }
        [TestMethod]
        public void TestHandleFloatNumberOperator()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5", calculatorState = new JsonResponse { IsOperator = true, IsMinus = true, Display = "-", State = "-" } };

            //act
            JsonResponse response = request.HandleFloatNumber(null);

            //assert
            response.Display.Should().Be("5");
            response.IsMinus.Should().Be(false);
            response.IsOperator.Should().Be(false);
        }
        [TestMethod]
        public void TestHandleFloatNumberNotOperator()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "5", calculatorState = new JsonResponse { IsOperator = false, IsMinus = true, Display = "-", State = "-" } };

            //act
            JsonResponse response = request.HandleFloatNumber(null);

            //assert
            response.Display.Should().Be("-5");
            response.IsMinus.Should().Be(false);
            response.IsOperator.Should().Be(false);
        }
        [TestMethod]
        public void TestHandleOperatorIsOperatorInputMinus()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "-", calculatorState = new JsonResponse { IsOperator = true, IsMinus = true, Display = "12", State = "100+12-" } };

            //act
            JsonResponse response = request.HandleOperator(null);

            //assert
            response.Display.Should().Be("-");
            response.IsMinus.Should().Be(true);
            response.IsOperator.Should().Be(false);
        }
        [TestMethod]
        public void TestHandleOperatorIsDoubleOperator()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "+", calculatorState = new JsonResponse { IsOperator = true, IsMinus = true, Display = "12", State = "100+12-" } };

            //act
            JsonResponse response = request.HandleOperator(null);

            //assert
            response.Display.Should().Be("");
            response.IsMinus.Should().Be(false);
            response.IsOperator.Should().Be(false);
        }
        [TestMethod]
        public void TestHandleOperatorEmptyStateOperator()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "+" };

            //act
            JsonResponse response = request.HandleOperator(null);

            //assert
            response.Display.Should().Be("");
            response.IsMinus.Should().Be(false);
            response.IsOperator.Should().Be(false);
        }
        [TestMethod]
        public void TestHandleOperatorEmptyStateOperatorMinus()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "-" };

            //act
            JsonResponse response = request.HandleOperator(null);

            //assert
            response.Display.Should().Be("-");
            response.IsMinus.Should().Be(true);
            response.IsOperator.Should().Be(false);
        }
    }


}
