using System;

namespace HappyBank.UseCases.OpenAccount
{
    public class OpenAccountOutput
    {
        public Guid AccountId{get; set;}
        public int AccountNumber{get; set;}
        public int BranchNumber {get; set;}
    }
}