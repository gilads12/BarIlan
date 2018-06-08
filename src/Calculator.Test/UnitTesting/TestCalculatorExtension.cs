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
            List<string> expected = new List<string> {"1","2","+","3","/"};

            //act
            List<Token> result = infixTokens.InfixToPostfix().ToList();
            List<string> resultString = new List<string>();
            foreach(var i in result)
            {
                switch(i)
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
        public void TestGetLastNumericFromString()
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
        public void TestIsInputValidNotValidInput()
        {
            //arrange 
            JsonRequest request = new JsonRequest { Input = "1+" };

            //act
            bool result = request.IsInputValid();

            //assert
            result.Should().Be(false);
        }
    }
}
