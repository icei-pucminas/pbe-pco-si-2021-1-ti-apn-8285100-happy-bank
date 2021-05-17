using System;

namespace HappyBank.Domain.Model
{
    public class Deposit : Transaction
    {
        public Deposit(Guid accountId, decimal value, DateTime executionTime, string envelopeCode)
            : base (accountId, value, executionTime, TransactionKind.DEPOSIT)
        {
            this.EnvelopeCode = envelopeCode;
        }
        public string EnvelopeCode {get; private set;}
    }
}