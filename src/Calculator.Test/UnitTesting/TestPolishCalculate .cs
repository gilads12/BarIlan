using Calculator.Core;
using Calculator.Core.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Test
{
    [TestClass]
    public class TestPolishCalculate
    {
        [TestMethod]
        public void TestReturnsToTopIfOnlyNumbers()
        {
            //arrange 
            int expected = 1;
            var calc = new PolishCalculate(new Token[] { new NumericToken(6), new NumericToken(5), new NumericToken(1) });

            //act
            int result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestSumCalculation()
        {
            //arrange 
            int expected = 9;
            var calc = new PolishCalculate(new Token[] { new NumericToken(6), new NumericToken(3), new OperatorToken('+') });

            //act
            int result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestComplexCalculation()
        {
            //arrange 
            int expected = 1;
            var calc = new PolishCalculate(new Token[] { new NumericToken(4), new NumericToken(3), new NumericToken(11), new NumericToken(22), new OperatorToken('/'), new OperatorToken('+'), new OperatorToken('-') });

            //act
            int result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestDivCalculation()
        {
            //arrange 
            int expected = 3;
            var calc = new PolishCalculate(new Token[] { new NumericToken(21), new NumericToken(7), new OperatorToken('/') });

            //act
            int result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivByZeroCalculation()
        {
            //arrange 
            var calc = new PolishCalculate(new Token[] { new NumericToken(14), new NumericToken(0), new OperatorToken('/') });

            //act
            int result = calc.Calculate();
        }

        [TestMethod]
        [ExpectedException(typeof(NoTokensException))]

        public void TestReturnNullEmptyCalculation()
        {
            //arrange 
            var calc = new PolishCalculate(new Token[] { });

            //act
            int result = calc.Calculate();
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidOperationException))]
        public void TestReturnNullNotEnoughNumerics()
        {
            //arrange 
            var calc = new PolishCalculate(new Token[] { new NumericToken(1), new OperatorToken('+') });

            //act
            int result = calc.Calculate();

            //assert
            Assert.IsNull(result);
        }
    }
}
