using Calculator.Core;
using Calculator.Core.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void TestReturnsNumericFromNegativeNumericString()
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
            string numeric = "1+4.4=";
            string expected = "4.4";

            //act
            string result = numeric.GetLastNumeric();

            //assert
            result.Should().Be(expected);
        }

        //[testmethod]
        //public void testsplitandkeep()
        //{
        //    //arrange 
        //    string str = "12+3+5=3=7-5*36";
        //    privatetype privatetype = new privatetype(typeof(myclass));


        //    //act
        //    privateobject obj = new privateobject(target);
        //    var retval = obj.invoke("privatemethod");
        //    assert.areequal(retval, expectedval);
        //    token result = str.splitandkeep(new char[] { '+', '5' });

        //    //assert
        //    result.should().beoftype(typeof(operatortoken));
        //}
    }
}
