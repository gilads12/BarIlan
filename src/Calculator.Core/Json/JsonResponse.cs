namespace Calculator.Core
{
    public class JsonResponse
    {
        public string State { get; set; } = default(string);
        public string Display { get; set; } = default(string);
        public bool IsOperator { get; set; } = false;//true for positive
        public bool IsMinus { get; set; } = false;
    }
}
