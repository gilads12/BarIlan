namespace Calculator.Core
{
    public class NumericToken : Token
    {
        public readonly float value; 

        public NumericToken(float value)
        {
            this.value = value;
        }
    }
}
