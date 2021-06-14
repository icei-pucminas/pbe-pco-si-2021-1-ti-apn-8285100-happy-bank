using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.FindAccount;

namespace HappyBank.Api.Controllers.FindAccount
{
    [ApiController]
    [Route("account")]
    public class FindAccountController
    {
         private readonly ILogger<FindAccountController> _logger;
         private readonly FindAccountUC _findAccountUC;

         public FindAccountController(ILogger<FindAccountController> logger, FindAccountUC findAccountUC)
         {
             _logger = logger;
             _findAccountUC = findAccountUC;
         }

        [HttpGet("find")]
        public FindAccountResponse Find([FromQuery]Guid? bankId, [FromQuery]int agencyNumber, [FromQuery]int accountNumber)
        {
            _logger.LogInformation($"Finding account: {bankId} - {agencyNumber}/{accountNumber}");

            var output = _findAccountUC.Execute(new FindAccountInput{
                BankId = bankId,
                AgencyNumber = agencyNumber,
                AccountNumber = accountNumber
            });

            return new FindAccountResponse{
                AccountId = output.AccountId,
                CustomerName = output.CustomerName,
                GovNumber = output.GovNumber
            };
        }   
    }
}