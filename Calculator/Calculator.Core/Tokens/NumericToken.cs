namespace Calculator.Core
{
    public class NumericToken : Token
    {
        public int _value;

        public NumericToken(int value)
        {
            _value = value;
        }
    }
}
