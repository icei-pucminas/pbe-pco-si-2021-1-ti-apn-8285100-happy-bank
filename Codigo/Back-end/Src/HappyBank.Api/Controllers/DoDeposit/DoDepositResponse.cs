using System;

namespace HappyBank.Api.Controllers.SignIn
{
    public class DoDepositResponse
    {
        public Guid TransactionId {get; set;}
        public string CustomerName {get; set;}
    }
}