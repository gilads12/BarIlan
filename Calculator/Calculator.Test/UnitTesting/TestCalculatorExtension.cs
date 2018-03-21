using Calculator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Test.UnitTesting
{
    [TestClass]
    public class TestCalculatorExtension
    {
        [TestMethod]
        public void TestReturnsNumericFromNumericChar()
        {
            //arrange 
            char numeric = '5';

            //act
            Token result = numeric.GetTokenFromChar();

            //assert
            Assert.IsInstanceOfType(result, typeof(NumericToken));
        }

        [TestMethod]
        public void TestReturnsNullFromNotNumericOrOperator()
        {
            //arrange 
            char numeric = '(';

            //act
            Token result = numeric.GetTokenFromChar();

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestReturnsOperatorFromOperatorChar()
        {
            //arrange 
            char numeric = '+';

            //act
            Token result = numeric.GetTokenFromChar();

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
