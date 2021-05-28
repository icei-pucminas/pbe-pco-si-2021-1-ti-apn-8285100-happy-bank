using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.DoWithdraw;
using HappyBank.Api.Services;

namespace HappyBank.Api.Controllers.DoWithdraw
{
    [ApiController]
    [Route("account")]
    public class DoWithdrawController
    {
        private readonly ILogger<DoWithdrawController> _logger;
        private readonly DoWithdrawUC _doWithdraw;
        private readonly ContextService _contextService;

        public DoWithdrawController(
            ILogger<DoWithdrawController> logger,
            DoWithdrawUC doWithdraw,
            ContextService contextService
        )
        {
            _logger = logger;
            _doWithdraw = doWithdraw;
            _contextService = contextService;
        }

        [HttpPost("withdraw")]
        public DoWithdrawResponse DoWithdraw(DoWithdrawRequest request)
        {
            _logger.LogInformation($"Doing withdraw for customer: {_contextService.CustomerId()}");

            var output = _doWithdraw.Execute(new DoWithdrawInput
            {
                CustomerId = _contextService.CustomerId(),
                Value = request.Value,
                TerminalCode = request.TerminalCode
            });

            if (null == output)
            {
                return null;
            }

            return new DoWithdrawResponse
            {
                TransactionId = output.TransactionId
            };
        }
    }
}
