using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.DoTransfer;
using HappyBank.Api.Services;

namespace HappyBank.Api.Controllers.DoTransfer
{
    [ApiController]
    [Route("account")]
    public class DoTransferController
    {
        private readonly ILogger<DoTransferController> _logger;
        private readonly DoTransferUC _doTransfer;
        private readonly ContextService _contextService;

        public DoTransferController(
            ILogger<DoTransferController> logger,
            DoTransferUC doTransfer,
            ContextService contextService
        )
        {
            _logger = logger;
            _doTransfer = doTransfer;
            _contextService = contextService;
        }

        [HttpPost("transfer")]
        public DoTransferResponse DoTransfer(DoTransferRequest request)
        {
            _logger.LogInformation($"Doing transfer for customer: {_contextService.CustomerId()}");

            var output = _doTransfer.Execute(new DoTransferInput
            {
                CustomerId = _contextService.CustomerId(),
                Value = request.Value,
                AccountDestinyId = request.AccountDestinyId
            });

            if (null == output)
            {
                return null;
            }

            return new DoTransferResponse
            {
                TransactionId = output.TransactionId
            };
        }
    }
}
