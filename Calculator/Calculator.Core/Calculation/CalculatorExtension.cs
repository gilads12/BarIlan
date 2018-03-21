using Calculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace Calculator.Core
{
    public static class CalculatorExtension
    {
        public static IEnumerable<Token> GetTokensFromJsonRequest(this JsonRequest request)
        {
            List<Token> tokens = new List<Token>();
            var lastExercise = request.CalculatorState.Split('=').Last();
            lastExercise.Split('-', '+', '/', '*').Union(Regex.Split(lastExercise, @"[\d]").Where(s => s != String.Empty)).ToObservable().Subscribe(ch => tokens.Add(ch.GetTokenFromString()));
            return tokens;
        }

        private static Regex _operatorRegex = new Regex(@"[+-/*]");

        public static Token GetTokenFromString(this string str)
        {
            if (str.All(char.IsDigit))
                return new NumericToken(int.Parse(str));
            if (_operatorRegex.Match(str[0].ToString()).Success)
                return new OperatorToken(str[0]);
            throw new NotValidTokenException();

        }
    }
}
