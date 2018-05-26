using Calculator.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core
{

    public class PolishCalculate
    {

        private IEnumerable<Token> m_tokens { get; set; }

        public PolishCalculate(IEnumerable<Token> tokens) => this.m_tokens = tokens;

        public float Calculate()
        {
            if (!this.m_tokens.Any())
                throw new NoTokensException();

            Stack<float> stack = new Stack<float>();

            foreach (var token in this.m_tokens)
            {
                Calculate(stack, token);
            }

            return stack.Pop();
        }

        private void Calculate(Stack<float> stack, Token token)
        {
            if (token is OperatorToken)
            {
                if (stack.Count < 2)
                    throw new NotValidOperationException("Not enuoth numeric");
                float left = stack.Pop();
                float right = stack.Pop();

                switch (((OperatorToken)token).value)
                {
                    case '-':
                        stack.Push(right - left);
                        break;
                    case '+':
                        stack.Push(right + left);
                        break;
                    case '*':
                        stack.Push(right * left);
                        break;
                    case '/':
                        stack.Push(right / left);
                        break;
                    default: throw new NotValidOperationException("Get 2 numeric Tokens without Operator");
                }
            }
            else
            {
                stack.Push(((NumericToken)token).value);
            }
        }
    }
}
