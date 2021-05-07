using System;

namespace HappyBank.Domain.Model
{
    public class Withdraw : Transaction
    {
        public Withdraw(Account account, decimal value, DateTime executionDate, string terminalCode) 
            : base (account, value, executionDate, TransactionKind.WITHDRAW)
        {
            this.TerminalCode = terminalCode;
        }
        public string TerminalCode {get; private set;}
    }
}