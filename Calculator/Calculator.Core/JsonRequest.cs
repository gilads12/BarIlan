using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return state.CalculatorState.Split('-', '+', '*', '/').FirstOrDefault();
        }

        public static string GetLastNumeric(this string state)
        {
            return state.Split('-', '+', '*', '/').FirstOrDefault();
        }


    }

    public class JsonResponse : JsonState
    {
        public string Display { get; set; }
    }
}
