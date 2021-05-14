using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HappyBank.UseCases.CustomerRegistration;

namespace HappyBank.Api.Controllers.CustomerRegistration
{
    [ApiController]
    [Route("customer")]
    public class CustomerRegistrationController
    {
        private readonly ILogger<CustomerRegistrationController> _logger;
        private readonly CustomerRegistrationUC _customerRegistrationUC;

        public CustomerRegistrationController(
            ILogger<CustomerRegistrationController> logger,
            CustomerRegistrationUC customerRegistrationUC)
        {
            _logger = logger;
            _customerRegistrationUC = customerRegistrationUC;
        }

        [HttpPost("register")]
        public CustomerRegistrationResponse Register(CustomerRegistrationRequest request)
        {
            _logger.LogInformation($"Registering customer {request.Email}");

            var output = _customerRegistrationUC.Execute(new CustomerRegistrationInput
            {
                Name = request.Name,
                GovNumber = request.GovNumber,
                Street = request.Street,
                District = request.District,
                City = request.City,
                State = request.State,
                AddressNumber = request.AddressNumber,
                BirthDate = request.BirthDate,
                Phone = request.Phone,
                Email = request.Email,
                Password = request.Password
            });

            return new CustomerRegistrationResponse
            {
                CustomerId = output.CustomerId
            };
        }
    }
}