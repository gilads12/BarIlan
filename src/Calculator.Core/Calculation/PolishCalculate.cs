using Calculator.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core
{

    public interface ICalculate// needed only for mocking PolishCalculate
    {
        int Calculate();
    }

    public class PolishCalculate:ICalculate
    {

        private IEnumerable<Token> m_tokens { get; set; }

        public PolishCalculate(IEnumerable<Token> tokens) => this.m_tokens = tokens;

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

        private void Calculate(Stack<int> stack, Token token)
        {
            if (token is OperatorToken)
            {
                if (stack.Count < 2)
                    throw new NotValidOperationException("Not enuoth numeric");
                int left = stack.Pop();
                int right = stack.Pop();

                switch (((OperatorToken)token)._value)
                {
                    case '-':
                        stack.Push(right  - left);
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
                stack.Push(((NumericToken)token)._value);
            }
        }
    }
}
