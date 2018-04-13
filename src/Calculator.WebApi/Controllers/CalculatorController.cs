using Calculator.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Calculator.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class CalculatorController : Controller
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            this._logger = logger;
            this._logger.LogInformation("Calculator is up");
        }

        [HttpPost("Calculate")]
        public IActionResult Calculate([FromBody]JsonRequest request)
        {
            if (null == request)
                return new BadRequestResult();
            _logger.LogInformation($"Get calculate request. CalculatorState: {request.CalculatorState}, Input: {request.Input}.");
            try
            {
                return this.Ok(request.CalculateNextState());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get bad request!");//to be changed
                return this.Ok(new JsonResponse { CalculatorState = request.CalculatorState, Display = default(string) });//TBD
            }
        }
    }
}
