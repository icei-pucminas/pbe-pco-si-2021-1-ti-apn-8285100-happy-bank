using System;

namespace HappyBank.UseCases.FindAccount
{
    public class FindAccountOutput
    {
        public Guid AccountId {get; set;}
        public string CustomerName {get; set;}
        public string GovNumber {get; set;}
    }
}