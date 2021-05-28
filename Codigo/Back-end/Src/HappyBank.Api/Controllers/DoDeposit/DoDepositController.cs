using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.DoDeposit;
using HappyBank.Api.Services;

namespace HappyBank.Api.Controllers.SignIn
{
    [ApiController]
    [Route("account")]
    public class DoDepositController
    {
        private readonly ILogger<DoDepositController> _logger;
        private readonly DoDepositUC _doDeposit;

        public DoDepositController(ILogger<DoDepositController> logger, DoDepositUC doDeposit)
        {
            _logger = logger;
            _doDeposit = doDeposit;
        }

        [HttpPost("deposit")]
        public DoDepositResponse Register(DoDepositRequest request)
        {
            _logger.LogInformation($"Doing deposit in account: {request.AccountNumber}/{request.AgencyNumber}");

            var output = _doDeposit.Execute(new DoDepositInput
            {
                AccountNumber = request.AccountNumber,
                AgencyNumber = request.AgencyNumber,
                Value = request.Value,
                EnvelopeCode = request.EnvelopeCode
            });

            if (null == output)
            {
                return null;
            }

            return new DoDepositResponse
            {
                TransactionId = output.TransactionId
            };
        }
    }
}