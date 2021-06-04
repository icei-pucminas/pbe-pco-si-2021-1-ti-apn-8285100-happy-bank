using System;

namespace HappyBank.UseCases.FindAccount
{
    public class FindAccountInput
    {
        public Guid? BankId {get; set;}
        public int AgencyNumber {get;set;}
        public int AccountNumber {get;set;}
    }
}