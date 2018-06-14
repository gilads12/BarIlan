namespace Calculator.Core
{
    public class JsonRequest 
    {
        public string Input { get; set; }
        public JsonResponse calculatorState { get; set; } = new JsonResponse();
    }
}
