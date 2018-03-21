﻿using Calculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Core
{

    public class PolishCalculate
    {

        private IEnumerable<Token> m_tokens { get; set; }

        public PolishCalculate(IEnumerable<Token> tokens)
        {
            this.m_tokens = tokens;
        }

        public int Calculate()
        {
            if (!this.m_tokens.Any())
                throw new NoTokensException();
            Stack<int> stack = new Stack<int>();

            foreach (var token in this.m_tokens)
            {
                Calculate(stack, token);
            }

            return stack.Pop();
        }

        void Calculate(Stack<int> stack, Token token)
        {
            if (token is OperatorToken)
            {
                if (stack.Count < 2)
                    return;
                int left = stack.Pop();
                int right = stack.Pop();

                switch (((OperatorToken)token)._value)
                {
                    case '-':
                        stack.Push(left - right);
                        break;
                    case '+':
                        stack.Push(left + right);
                        break;
                    case '*':
                        stack.Push(left * right);
                        break;
                    case '/':
                        stack.Push(left / right);
                        break;
                    default: throw new NotValidOperationException("Get 2 numeric Tokens without Operator");
                }
            }
            else
            {
                stack.Push(((NumericToken)token)._value);
            }
        }
    }
}