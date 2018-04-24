using Calculator.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace Calculator.Core
{
    public static class CalculatorExtension
    {
        private static Regex _operatorRegex = new Regex(@"[+-/*=]");
        private static Regex _floatRegex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        public static IEnumerable<Token> GetTokensFromJsonRequest(this JsonRequest request) => request.CalculatorState.Replace("=","").GetTokensFromString().InfixToPostfix();
        public static bool IsOperator(this string str) => str == null ? false : str.Length == 1 && _operatorRegex.Match(str[0].ToString()).Success;
        public static bool IsFloatNumber(this string str) => str == null ? false : _floatRegex.IsMatch(str);
        public static Token ToToken(this string str)
        {
            if (str.IsFloatNumber())
                return new NumericToken(float.Parse(str));
            if (str.IsOperator())
                return new OperatorToken(str[0]);
            throw new NotValidTokenException(str);
        }

        private static IEnumerable<string> SplitAndKeep(this string s, char[] delims)
        {
            int start = 0, index;

            while ((index = s.IndexOfAny(delims, start)) != -1)
            {
                if (index - start > 0)
                    yield return s.Substring(start, index - start);
                yield return s.Substring(index, 1);
                start = index + 1;
            }

            if (start < s.Length)
            {
                yield return s.Substring(start);
            }
        }
        private static IEnumerable<Token> InfixToPostfix(this IEnumerable<Token> infixTokens)
        {
            var stack = new Stack<Token>();
            var postfix = new Stack<Token>();

            foreach (var token in infixTokens)
            {
                if (token is OperatorToken)
                {
                    postfix.Push(token);
                }
                else
                {
                    if (postfix.Count > 0)
                    {
                        stack.Push(token);
                        while (postfix.Count > 0)
                            stack.Push(postfix.Pop());
                    }
                    else stack.Push(token);
                }
            }

            while (stack.Count > 0)
            {
                postfix.Push(stack.Pop());
            }

            return postfix;
        }
        private static IEnumerable<Token> GetTokensFromString(this string str)
        {
            foreach (var s in str.SplitAndKeep(new[] { '*', '/', '+', '-', '=' }))
            {
                yield return s.ToToken();
            }
        }

    }
}
