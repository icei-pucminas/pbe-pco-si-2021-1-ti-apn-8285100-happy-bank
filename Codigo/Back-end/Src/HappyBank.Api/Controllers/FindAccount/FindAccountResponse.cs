using System;

namespace HappyBank.Api.Controllers.FindAccount
{
    public class FindAccountResponse
    {
        public Guid AccountId {get; set;}
        public string CustomerName {get; set;}
        public string GovNumber {get; set;}
    }
}