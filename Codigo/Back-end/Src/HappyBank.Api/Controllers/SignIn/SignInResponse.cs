using System;

namespace HappyBank.Api.Controllers.SignIn
{
    public class SignInResponse
    {
        public Guid CustomerId {get; set;}
        public int AgencyNumber {get; set;}
        public int AccountNumber {get; set;}
    }
}