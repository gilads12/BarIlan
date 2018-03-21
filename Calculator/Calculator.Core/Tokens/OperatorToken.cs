namespace Calculator.Core
{
    public class OperatorToken :Token
    {
        public char _value;

        public OperatorToken(char value)
        {
            _value = value;
        }
    }
}
