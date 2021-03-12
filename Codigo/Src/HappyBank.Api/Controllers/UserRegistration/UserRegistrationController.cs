using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.UserRegistration;

namespace HappyBank.Api.Controllers.UserRegistration
{
    [ApiController]
    [Route("user")]
    public class UserRegistrationController
    {
        private readonly ILogger<UserRegistrationController> _logger;
        private readonly UserRegistrationUC _userRegistrationUC;

        public UserRegistrationController(
            ILogger<UserRegistrationController> logger,
            UserRegistrationUC userRegistrationUC)
        {
            _logger = logger;
            _userRegistrationUC = userRegistrationUC;
        }

        [HttpPost("register")]
        public UserRegistrationResponse Register(UserRegistrationRequest request)
        {
            _logger.LogInformation($"Registering user {request.Username}");

            var output =_userRegistrationUC.Execute(new UserRegistrationInput {
                Name = request.Name,
                Username = request.Username
            });

            return new UserRegistrationResponse{
                UserId = output.UserId
            };
        }   
    }
}