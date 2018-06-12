﻿using Calculator.Core.Exceptions;
using System;
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

        public static bool IsFloatNumber(this string str) => str == null ? false : _floatRegex.IsMatch(str);
        public static bool IsOperator(this string str) => str == null ? false : str.Length == 1 && _operatorRegex.Match(str[0].ToString()).Success;
        public static bool IsInputValid(this JsonRequest request) => ((request.Input.IsOperator() && request.Input.Length == 1) || request.Input.IsFloatNumber());
        public static Token ToToken(this string str)
        {
            if (str.IsFloatNumber())
                return new NumericToken(float.Parse(str));
            if (str.IsOperator())
                return new OperatorToken(str[0]);
            throw new NotValidTokenException(str);
        }
        /// <summary>
        /// return the last numeric on the string (without sign)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="fromEnd">True search for the last numeric from the end (129+ return default(string)) False search the last numeric from the begin of state </param>
        /// <returns></returns>
        public static string GetLastNumeric(this string state, bool fromEnd = false)
        {
            if (fromEnd)
                return state?.Split( '-', '+', '*', '/', '=' ).LastOrDefault();
           return  state?.Split(new[] { '-', '+', '*', '/', '=', ' ' }, StringSplitOptions.RemoveEmptyEntries).Last() ?? "0";
        }
        public static IEnumerable<Token> GetTokensFromJsonRequest(this JsonRequest request) => request?.calculatorState.State.PreProcessingString()?.GetTokensFromString().PostProcessingTokens();
        public static IEnumerable<string> SplitAndKeep(this string s, char[] delims)
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
        public static IEnumerable<Token> InfixToPostfix(this IEnumerable<Token> infixTokens)
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
        public static string LastNumericSign(this string state)// if end with '--', '+-', '/-', '*-' return -
        {
            state = "0+" + state;
            int lastOperatorIndex = state.LastIndexOfAny(new[] { '-', '/', '+', '*' });
            if (state[lastOperatorIndex] == '-' &&
                (state[lastOperatorIndex - 1] == '+' || state[lastOperatorIndex - 1] == '-')
                || state[lastOperatorIndex - 1] == '/' || state[lastOperatorIndex - 1] == '*')
                return "-";
            return "+";
        }
        public static string PadLeft(this string str, string pad)
        {
            return pad + str;
        }
        /// <summary>
        /// Take care of +- , -- , /- , *- and insert the - to the next numeric
        /// (importent! -+ , -/ , -* is invalid)
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static IEnumerable<Token> InsertMinusToToken(this IEnumerable<Token> tokens)
        {
            IEnumerator<Token> enumerator = tokens.GetEnumerator();
            bool negative = false, lastTokenOperator = false;
            while (enumerator.MoveNext())
            {
                Token token = enumerator.Current;
                switch (token)
                {
                    case NumericToken n:
                        lastTokenOperator = false;
                        if (!negative)
                            yield return n;
                        else
                        {
                            negative = false;
                            yield return new NumericToken(n.value * -1);
                        }
                        break;
                    case OperatorToken o:
                        if (lastTokenOperator)
                        {
                            if (o.value == '-')
                            {
                                negative = true;
                                lastTokenOperator = false;
                            }
                        }
                        else
                        {
                            lastTokenOperator = true;
                            yield return o;
                        }
                        break;
                }
            }
        }


        private static string PreProcessingString(this string str) => str.PadLeft("0+").Replace("=", "");
        private static IEnumerable<Token> PostProcessingTokens(this IEnumerable<Token> token) => token.InsertMinusToToken().InfixToPostfix();
        private static IEnumerable<Token> GetTokensFromString(this string str)
        {
            foreach (var s in str.SplitAndKeep(new[] { '*', '/', '+', '-', '=' }))
            {
                yield return s.ToToken();
            }
        }
    }
}
