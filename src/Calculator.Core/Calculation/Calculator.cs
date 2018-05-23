using Calculator.Core.Exceptions;

namespace Calculator.Core
{
    // static class that has no state !!!
    public static class Calculator
    {
        public static CalculatorState CalculateNextState(this JsonRequest request)
        {
            if (!request.IsInputValid())
                throw new NotValidInput(request.Input);

            if (request.calculatorState.State == default(string))
                if (request.Input != default(string) && request.Input.IsOperator())
                    throw new NotValidInput(request.Input);
                else return new CalculatorState { Display = request.Input, State = request.Input };

            if (request.Input == "=")
            {
                var tokens = request.GetTokensFromJsonRequest();
                return new CalculatorState { State = request.calculatorState.State + request.Input, Display = new PolishCalculate(tokens).Calculate().ToString() };
            }

            else
            {
                if (request.Input.IsOperator())
                    return new CalculatorState { State = request.calculatorState.State + request.Input, Display = request.calculatorState.State.GetLastNumeric() };
                else return new CalculatorState { State = request.calculatorState.State + request.Input, Display = request.calculatorState.State.GetLastNumeric() + request.Input };
            }
        }
    }
}
