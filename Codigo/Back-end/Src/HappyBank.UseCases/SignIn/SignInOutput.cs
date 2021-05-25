using System;

namespace HappyBank.UseCases.SignIn
{
    public class SignInOutput
    {
        public Guid CustomerId {get; set;}
        public int AgencyNumber {get; set;}
        public int AccountNumber {get; set;}
        public string CustomerName {get; set;}
        public string CustomerEmail {get; set;}
    }
}