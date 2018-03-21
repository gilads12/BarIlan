namespace Calculator.Core
{
    public class Calculator
    {
        JsonResponse CalculateNextState(JsonRequest request)
        {

            if (request.CalculatorState == null)
                return new JsonResponse { CalculatorState = request.Input, Display = request.Input };

            if (request.Input == "=")
            {
                var tokens = request.GetTokensFromJsonRequest();
                return new JsonResponse { CalculatorState = request.CalculatorState + request.Input, Display = new PolishCalculate(tokens).Calculate().ToString() };
            }

            else return new JsonResponse { CalculatorState = request.CalculatorState + request.Input, Display = request.GetLastNumeric()+request.Input };
        }
    }
}
