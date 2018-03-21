using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace Calculator.Core
{
    public static class CalculatorExtension
    {
        public static IEnumerable<Token> GetTokensFromJsonRequest(this JsonRequest request)
        {
            List<Token> tokens = new List<Token>();
            request.CalculatorState.ToObservable().Subscribe(ch => tokens.Add(ch.GetTokenFromChar()));
            return tokens;
        }

        private static Regex _operatorRegex = new Regex(@"+-/*");

        public static Token GetTokenFromChar(this char ch)
        {
            if (char.IsNumber(ch))
                return new NumericToken(ch);
            return new OperatorToken(ch);

        }
    }
}
