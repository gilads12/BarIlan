using System.Linq;

namespace Calculator.Core
{
    public static class JsonExtension
    {
        public static bool IsInputValid(this JsonRequest request) => ((request.Input.IsOperator() && request.Input.Length == 1) || request.Input.IsFloatNumber());
        public static string GetLastNumeric(this JsonState state) => GetLastNumeric(state.State);
        public static string GetLastNumeric(this string state) => state.Split('-', '+', '*', '/', '=').LastOrDefault().EmptyToNull();
        public static string EmptyToNull(this string str) => str == string.Empty ? null : str;
    }
}
