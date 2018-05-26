using Calculator.Core;
using Calculator.Core.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class TestCalculatorExtension
    {
        [TestMethod]
        public void TestReturnsNumericFromNumericString()
        {
            //arrange 
            string numeric = "5";

            //act
            Token result = numeric.ToToken();

            //assert
            result.Should().BeOfType(typeof(NumericToken));
        }
        [TestMethod]
        public void TestReturnsNumericFromFloatNumericString()
        {
            //arrange 
            string numeric = ".5";

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
        public void TestReturnsOperatorFromOperatorChar()
        {
            //arrange 
            string numeric = "+";

            //act
            Token result = numeric.ToToken();

            //assert
            result.Should().BeOfType(typeof(OperatorToken));
        }
        [TestMethod]
        public void TestIsOperatorFromOperatorAndNumeric()
        {
            //arrange 
            string numeric = "+4";

            //act
            bool result = numeric.IsOperator();

            //assert
            result.Should().Be(false);
        }
        [TestMethod]
        public void TestGetLaastNumericFromString()
        {
            //arrange 
            string numeric = "1+4.4";
            string expected = "4.4";

            //act
            string result = numeric.GetLastNumeric();

            //assert
            result.Should().Be(expected);
        }
        [TestMethod]
        public void TestGetTokenFromNegativeNumver()
        {
            //arrange 
            JsonRequest request = new JsonRequest { calculatorState = new JsonResponse { State = "-3+8--1" } };
            IEnumerable<Token> expected = new List<Token> { new NumericToken(-3), new OperatorToken('+'), new NumericToken(8), new OperatorToken('-'), new NumericToken(-1) };

            //act
            var result = request.GetTokensFromJsonRequest();

            //assert
            result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

    }
}
