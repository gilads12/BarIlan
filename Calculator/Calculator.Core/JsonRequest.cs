using System.Linq;

namespace Calculator.Core
{
    public abstract class JsonState
    {
        public string CalculatorState { get; set; }
    }
    public class JsonRequest : JsonState
    {
        public string Input { get; set; }
    }

    public static class JsonExtension
    {
        public static string GetLastNumeric(this JsonState state)
        {
            return GetLastNumeric(state.CalculatorState);
        }

        public static string GetLastNumeric(this string state)
        {
            return state.Split('-', '+', '*', '/','=').Last();
        }


    }

    public class JsonResponse : JsonState
    {
        public string Display { get; set; }
    }
}
