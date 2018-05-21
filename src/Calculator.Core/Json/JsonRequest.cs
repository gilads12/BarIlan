namespace Calculator.Core
{
    public class JsonRequest : JsonState
    {
        public string Input { get; set; }
        public CalculatorState calculatorState { get; set; }
    }

    public class CalculatorState
    {
        public string Display { get; set; }
    }
}
