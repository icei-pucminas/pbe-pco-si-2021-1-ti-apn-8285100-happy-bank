using System;

namespace HappyBank.Domain.Model
{
    public abstract class Transaction : Entity
    {
        protected Transaction(Guid accountId, decimal value, DateTime executionTime, TransactionKind kind)
        {
            this.Id = Guid.NewGuid();
            this.AccountId = accountId;
            this.Value = value;
            this.Kind = kind;
            this.ExecutionDate = executionTime;
        }
        
        public Guid AccountId {get; private set;}
        public TransactionKind Kind {get; private set;}
        public decimal Value {get; private set;}
        public DateTime ExecutionDate {get; private set;}
    }

    public enum TransactionKind
    {
        DEPOSIT, /*0*/
        WITHDRAW, /*1*/
        TRANSFER /*2*/
    }
}