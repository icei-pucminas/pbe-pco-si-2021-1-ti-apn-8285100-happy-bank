using System;

namespace HappyBank.UseCases.OpenAccount
{
    public class OpenAccountInput
    {
        public Guid UserId {get; set;}
        public int BranchNumber {get; set;}
    }
}