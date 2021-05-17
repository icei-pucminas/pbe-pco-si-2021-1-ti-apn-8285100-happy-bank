using System;

namespace HappyBank.Api.Controllers.OpenAccount
{
    public class OpenAccountResponse
    {
        public Guid AccountId{get; set;}
        public int AccountNumber{get; set;}
        public int AgencyNumber {get; set;}
    }
}