using System;

namespace HappyBank.Domain.Model
{
    public class Deposit : Transaction
    {
        public Deposit(Account account, decimal value, DateTime executionTime, string envelopeCode)
            : base (account, value, executionTime, TransactionKind.DEPOSIT)
        {
            this.EnvelopeCode = envelopeCode;
        }
        public string EnvelopeCode {get; private set;}
    }
}