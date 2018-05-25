namespace Calculator.Core
{
    public class OperatorToken :Token
    {
        public readonly char value;

        public OperatorToken(char value)
        {
            this.value = value;
        }
    }
}
