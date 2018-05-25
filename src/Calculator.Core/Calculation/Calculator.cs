using Calculator.Core.Exceptions;

namespace Calculator.Core
{
    public static class Calculator
    {
        public static JsonResponse CalculateNextState(this JsonRequest request)
        {
            if (!request.IsInputValid())
                throw new NotValidInput(request.Input);

            if (request.calculatorState?.State == default(string))
                if (request.Input != default(string) && request.Input.IsOperator())
                    throw new NotValidInput(request.Input);
                else return new JsonResponse { Display = request.Input, State = request.Input };

            if (request.Input == "=")
            {
                var tokens = request.GetTokensFromJsonRequest();
                return new JsonResponse { State = new PolishCalculate(tokens).Calculate().ToString(), Display = new PolishCalculate(tokens).Calculate().ToString() };
            }

            else
            {
                if (request.Input.IsOperator())
                    return new JsonResponse { State = request.calculatorState.State + request.Input, Display = request.calculatorState.State.GetLastNumeric() };
                else return new JsonResponse { State = request.calculatorState.State + request.Input, Display = request.calculatorState.State.GetLastNumeric() + request.Input };
            }
        }
    }
}
