using System;

namespace HappyBank.Domain.Model
{
    public class Withdraw : Transaction
    {
        public Withdraw(Guid accountId, decimal value, DateTime executionDate, string terminalCode) 
            : base (accountId, value, executionDate, TransactionKind.WITHDRAW)
        {
            this.TerminalCode = terminalCode;
        }
        public string TerminalCode {get; private set;}
    }
}