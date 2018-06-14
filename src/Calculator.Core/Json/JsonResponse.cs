namespace Calculator.Core
{
    public class JsonResponse
    {
        public string State { get; set; }
        public string Display { get; set; }
        public bool IsOperator { get; set; } = false;//true for positive
        public bool IsMinus { get; set; } = false;
    }
}
