using Calculator.Core;
using Calculator.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class TestCalculatorExtension
    {
        [TestMethod]
        public void TestReturnsNumericFromNumericChar()
        {
            //arrange 
            string numeric = "5";

            //act
            Token result = numeric.ToToken();

            //assert
            Assert.IsInstanceOfType(result, typeof(NumericToken));
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidTokenException))]

        public void TestReturnsNullFromNotNumericOrOperator()
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
            Assert.IsInstanceOfType(result, typeof(OperatorToken));
        }

        //[TestMethod]
        //public void TestReturnsNumericFromNotNumericBiggerThenNine()
        //{
        //    //arrange 
        //    char numeric = '11';

        //    //act
        //    Token result = numeric.GetTokenFromChar();

        //    //assert
        //    Assert.IsNull(result);
        //}
    }
}
