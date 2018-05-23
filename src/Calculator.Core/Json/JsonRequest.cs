namespace Calculator.Core
{
    public class JsonRequest 
    {
        public string Input { get; set; }
        public CalculatorState calculatorState { get; set; }
    }

    public class CalculatorState
    {
        public string State { get; set; }
        public string Display { get; set; }
    }
}
