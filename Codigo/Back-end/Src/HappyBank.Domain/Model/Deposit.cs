using System;

namespace HappyBank.Domain.Model
{
    public class Deposit : Transaction
    {
        public Deposit(Guid id, Guid accountId, decimal value, DateTime executionTime, string envelopeCode)
            : base(accountId, value, executionTime, TransactionKind.DEPOSIT)
        {
            this.Id = id;
            this.EnvelopeCode = envelopeCode;
        }
        public Deposit(Guid accountId, decimal value, DateTime executionTime, string envelopeCode)
            : base(accountId, value, executionTime, TransactionKind.DEPOSIT)
        {
            this.EnvelopeCode = envelopeCode;
        }
        public string EnvelopeCode { get; set; }
    }
}