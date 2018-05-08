using Calculator.Core.Exceptions;

namespace Calculator.Core
{
    // static class that has no state !!!
    public static class Calculator
    {
        public static JsonResponse CalculateNextState(this JsonRequest request)
        {
            if (!request.IsInputValid())
                throw new NotValidInput(request.Input);

            if (request.CalculatorState == default(string))
                if (request.Input != default(string) && request.Input.IsOperator())
                    throw new NotValidInput(request.Input);
                else return new JsonResponse { Display = request.Input };

            if (request.Input == "=")
            {
                var tokens = request.GetTokensFromJsonRequest();
                return new JsonResponse { Display = new PolishCalculate(tokens).Calculate().ToString() };
            }

            else
            {
                if (request.Input.IsOperator())
                    return new JsonResponse { Display = request.GetLastNumeric() };
                else return new JsonResponse {Display = request.GetLastNumeric() + request.Input };
            }
        }
    }
}
