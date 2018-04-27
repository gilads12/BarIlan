namespace Calculator.Core
{
    public class NumericToken : Token
    {
        public float _value;

        public NumericToken(float value)
        {
            _value = value;
        }
    }
}
