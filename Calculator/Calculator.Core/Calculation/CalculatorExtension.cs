using Calculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator.Core
{
    public static class CalculatorExtension
    {
        private static Regex _operatorRegex = new Regex(@"[+-/*=]");

        public static IEnumerable<Token> GetTokensFromJsonRequest(this JsonRequest request)
        {
            return request.CalculatorState.GetTokensFromString().InfixToPostfix();
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
                    while (stack.Count > 0)
                    {
                        postfix.Push(stack.Pop());
                    }
                    stack.Push(token);
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
        private static bool IsOperator(this string str)
        {
            if (_operatorRegex.Match(str[0].ToString()).Success)
                return true;
            return false;
        }
        public static Token ToToken(this string str)
        {
            if (str.All(char.IsDigit))
                return new NumericToken(int.Parse(str));
            if (str.IsOperator())
                return new OperatorToken(str[0]);
            throw new NotValidTokenException();
        }

    }
}
