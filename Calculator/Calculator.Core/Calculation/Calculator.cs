namespace Calculator.Core
{


    public static class Calculator
    {
        public static JsonResponse CalculateNextState(this JsonRequest request)
        {

            if (request.CalculatorState == default(string))
                if (request.Input != default(string) && request.Input.IsOperator())
                    return new JsonResponse { CalculatorState = request.Input, Display = default(string) };
                else return new JsonResponse { CalculatorState = request.Input, Display = request.Input };

            if (request.Input == "=")
            {
                var tokens = request.GetTokensFromJsonRequest();
                return new JsonResponse { CalculatorState = request.CalculatorState + request.Input, Display = new PolishCalculate(tokens).Calculate().ToString() };
            }

            else
            {
                if (request.Input.IsOperator())
                    return new JsonResponse { CalculatorState = request.CalculatorState + request.Input, Display = request.GetLastNumeric() };
                else return new JsonResponse { CalculatorState = request.CalculatorState + request.Input, Display = request.GetLastNumeric() + request.Input };
            }
        }
    }
}
