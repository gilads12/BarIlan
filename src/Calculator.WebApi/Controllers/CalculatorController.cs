﻿using Calculator.Core;
using Calculator.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Calculator.WebApi.Controllers
{
    [Produces("application/json")]
    public class CalculatorController : Controller
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("Calculate")]
        public IActionResult Calculate([FromBody]JsonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ModelState.Values.First().Errors.First().ErrorMessage
                });
            }

            if (null == request)
                return new BadRequestResult();
            _logger.LogInformation($"Get calculate request. CalculatorState: {request.calculatorState?.State}, Input: {request.Input}.");
            try
            {
                return this.Ok(request.CalculateNextState());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return this.Ok(JsonRequestExtension.InitialResponse());
            }
            
        }
    }
}
