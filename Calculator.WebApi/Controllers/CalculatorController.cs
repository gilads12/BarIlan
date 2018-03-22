using Calculator.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            this._logger = logger;
        }

        // POST api/
        [HttpPost]
        public JsonResponse Calculate([FromBody]JsonRequest request)
        {
             _logger.LogInformation("get calculate request");//to be changed
            try
            {
                return request.CalculateNextState();
            }
            catch
            {
                return new JsonResponse { CalculatorState = request.CalculatorState, Display = default(string) };//TBD
            }
        }
    }
}
