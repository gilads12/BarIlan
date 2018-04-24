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
            float expected = 1;
            var calc = new PolishCalculate(new Token[] { new NumericToken(6), new NumericToken(5), new NumericToken(1) });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestSumCalculation()
        {
            //arrange 
            float expected = 9;
            var calc = new PolishCalculate(new Token[] { new NumericToken(6), new NumericToken(3), new OperatorToken('+') });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestComplexCalculation()
        {
            //arrange 
            float expected = 0.5f;
            var calc = new PolishCalculate(new Token[] { new NumericToken(4), new NumericToken(3), new NumericToken(11), new NumericToken(22), new OperatorToken('/'), new OperatorToken('+'), new OperatorToken('-') });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestDivCalculation()
        {
            //arrange 
            float expected = 3;
            var calc = new PolishCalculate(new Token[] { new NumericToken(21), new NumericToken(7), new OperatorToken('/') });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);
        }

        [TestMethod]
        public void TestDivByZeroCalculation()
        {
            //arrange 
            bool flag = true;
            var calc = new PolishCalculate(new Token[] { new NumericToken(14), new NumericToken(0), new OperatorToken('/') });

            //act
            float result = calc.Calculate();

            //assert
            flag.Should().Be(float.IsInfinity(result));
        }

        [TestMethod]
        [ExpectedException(typeof(NoTokensException))]

        public void TestReturnNullEmptyCalculation()
        {
            //arrange 
            var calc = new PolishCalculate(new Token[] { });

            //act
            float result = calc.Calculate();
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidOperationException))]
        public void TestReturnNullNotEnoughNumerics()
        {
            //arrange 
            var calc = new PolishCalculate(new Token[] { new NumericToken(1), new OperatorToken('+') });

            //act
            float result = calc.Calculate();

            //assert
            calc.Should().BeNull();
        }

       [TestMethod]
       public void TestFloatResoult()
        {
            //arrange 
            float expected = 2.5f;
            var calc = new PolishCalculate(new Token[] { new NumericToken(10), new NumericToken(4), new OperatorToken('/') });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);

        }
        [TestMethod]
        public void TestFloatParameter()
        {
            //arrange 
            float expected = 4;
            var calc = new PolishCalculate(new Token[] { new NumericToken(14), new NumericToken(3.5f), new OperatorToken('/') });

            //act
            float result = calc.Calculate();

            //assert
            expected.Should().Be(result);

        }
    }
}
