using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.SignIn;
using HappyBank.Api.Services;

namespace HappyBank.Api.Controllers.SignIn
{
    [ApiController]
    [Route("customer")]
    public class SignInController
    {
        private readonly ILogger<SignInController> _logger;
        private readonly SignInUC _signInUC;

        public SignInController(ILogger<SignInController> logger, SignInUC signInUC)
        {
            _logger = logger;
            _signInUC = signInUC;
        }

        [HttpPost("signin")]
        public SignInResponse Register(SignInRequest request)
        {
            _logger.LogInformation($"Signing in customer: {request.Email}");

            var output = _signInUC.Execute(new SignInInput{
                Email = request.Email,
                Password = request.Password
            });

            if(null == output)
            {
                return null;
            }

            return new SignInResponse{
                CustomerId = output.CustomerId,
                AccountNumber = output.AccountNumber,
                AgencyNumber = output.AgencyNumber
            };
        }   
    }
}