using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.Api.Services;
using HappyBank.UseCases.ExtractStatement;
using HappyBank.UseCases.ExtractBalance;

namespace HappyBank.Api.Controllers.ExtractStatement
{
    [ApiController]
    [Route("account")]
    public class ExtractStatementController
    {
        private readonly ILogger<ExtractStatementController> _logger;
        private readonly ContextService _contextService;

        private readonly ExtractStatementUC _extractStatementUC;
        private readonly ExtractBalanceUC _extractBalanceUC;


        public ExtractStatementController(
            ILogger<ExtractStatementController> logger,
            ContextService contextService,
            ExtractStatementUC extractStatementUC,
            ExtractBalanceUC extractBalanceUC)
        {
            _logger = logger;
            _contextService = contextService;
            _extractStatementUC = extractStatementUC;
            _extractBalanceUC = extractBalanceUC;
        }

        [HttpGet("extractstatement")]
        public ExtractStatementResponse GetExtractStatement([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            _logger.LogInformation($"Geting extract statement of customer: {_contextService.CustomerId()}");

            var customerId = _contextService.CustomerId();

            var output = _extractStatementUC.Execute(new ExtractStatementInput
            {
                 CustomerId = customerId,
                 Start = start,
                 End = end
            });

            var response = new ExtractStatementResponse();
            output.ForEach(e => {
                response.Add(new ExtractStatementItem{
                    Id = e.Id,
                    Description = e.Description,
                    ExecutionDate = e.ExecutionDate,
                    Value = e.Value
                });
            });

            return response;
        }

        [HttpGet("extractbalance")]
        public decimal GetExtractBalance([FromQuery] DateTime? date)
        {
            _logger.LogInformation($"Geting balance of customer: {_contextService.CustomerId()}");

            var customerId = _contextService.CustomerId();

            return _extractBalanceUC.Execute(new ExtractBalanceInput
            {
                 CustomerId = customerId,
                 Date = date
            });
        }
    }
}