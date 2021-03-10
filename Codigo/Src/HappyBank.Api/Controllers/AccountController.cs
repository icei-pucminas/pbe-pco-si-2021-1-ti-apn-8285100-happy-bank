using System.Collections.Generic;
using HappyBank.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HappyBank.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            _logger.LogInformation("Listing accounts");

            return new List<Account>();
        }   
    }
}