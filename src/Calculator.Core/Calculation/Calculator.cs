using Calculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core
{
    public static class Calculator
    {
        public static JsonResponse CalculateNextState(this JsonRequest request)
        {
            //if (!request.IsRequestValid())//todo
            //throw new NotValidInput(request.Input);// rename to not valid requst

            if (request.Input == "=")
            {
                var tokens = request.GetTokens();
                var result = new PolishCalculate(tokens).Calculate().ToString();
                return new JsonResponse { State = result, Display = result, IsOperator = false };
            }
            else
            {
                var state = request.GetState();
                 if (request.Input.IsOperator())
                    return request.HandleOperator(state);
                else if (request.Input.IsFloatNumber())
                    return request.HandleFloatNumber(state);
                return JsonRequestExtension.InitialResponse();
            }

        }

    }

    public static class JsonRequestExtension
    {
        public static string GetState(this JsonRequest request)
        {
            return request.calculatorState?.State + request.Input;
        }

        public static JsonResponse HandleFloatNumber(this JsonRequest request, string state)
        {
            if (request.calculatorState.IsOperator)
                return new JsonResponse { Display = request.Input, IsOperator = false, State = state };
            return new JsonResponse { Display = request.calculatorState?.Display + request.Input, IsOperator = false, State = state };
        }

        public static JsonResponse HandleOperator(this JsonRequest request, string state)
        {
            if (request.calculatorState.IsOperator)
            {
                if (request.Input == "-")
                    return new JsonResponse { Display = "-", State = state, IsOperator = false, IsMinus = true };
                if (request.Input.IsOperator())
                    return InitialResponse();

            }
            else if (request.calculatorState.IsMinus || (request.calculatorState.State == default(string) && request.Input != "-"))
                return InitialResponse();
            else if (!request.calculatorState.IsMinus && request.Input == "-" && string.IsNullOrEmpty(request.calculatorState.State))
                return new JsonResponse { Display = request.Input, IsMinus = true, IsOperator = false, State = state };
            return new JsonResponse { Display = request.calculatorState.Display, IsOperator = true, IsMinus = false, State = state };
        }

        public static JsonResponse InitialResponse() => new JsonResponse { Display = "", IsOperator = false, IsMinus = false, State = "" };

        public static bool IsRequestValid(this JsonRequest request)
        {

            if (request == null || request.Input == default(string))
                return false;
            if (!request.Input.IsFloatNumber() && !request.Input.IsOperator())
                return false;
            if (request.Input.IsOperator() && request.Input != "-")
            {
                if (request.calculatorState == null || request.calculatorState.State == default(string))
                    return false;
            }
            var fullstate = request.calculatorState?.State + request.Input;//todo finish
            return true;
        }

        public static IEnumerable<Token> GetTokens(this JsonRequest request) => request?.calculatorState.State.Replace("=", "").GetTokensFromString().PostProcessingTokens();
    }
}
