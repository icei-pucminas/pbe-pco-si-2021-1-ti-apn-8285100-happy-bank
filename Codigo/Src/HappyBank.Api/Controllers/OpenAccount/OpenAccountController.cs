using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.OpenAccount;
using HappyBank.Api.Services;

namespace HappyBank.Api.Controllers.OpenAccount
{
    [ApiController]
    [Route("account")]
    public class OpenAccountController
    {
        private readonly ILogger<OpenAccountController> _logger;
        private readonly ContextService _contextService;
        private readonly OpenAccountUC _openAccountUC;
        
        
        public OpenAccountController(
            ILogger<OpenAccountController> logger,
            ContextService contextService,
            OpenAccountUC openAccountUC)
        {
            _logger = logger;
            _contextService = contextService;
            _openAccountUC = openAccountUC;
        }

        [HttpPost("open")]
        public OpenAccountResponse Register(OpenAccountRequest request)
        {
            _logger.LogInformation($"Opening account customer: {_contextService.CustomerId()}");

            var customerId = _contextService.CustomerId();

            var output = _openAccountUC.Execute(new OpenAccountInput{
                CustomerId = customerId
            });

            return new OpenAccountResponse{
                AccountId = output.AccountId,
                AccountNumber = output.AccountNumber,
                AgencyNumber = output.AgencyNumber
            };
        }   
    }
}