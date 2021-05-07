using System;

namespace HappyBank.Domain.Model
{
    public abstract class Transaction : Entity
    {
        protected Transaction(Account account, decimal value, DateTime executionTime, TransactionKind kind)
        {
            this.Id = Guid.NewGuid();
            this.Account = account;
            this.Value = value;
            this.Kind = kind;
            this.ExecutionDate = executionTime;
        }
        
        public Account Account {get; private set;}
        public TransactionKind Kind {get; private set;}
        public decimal Value {get; private set;}
        public DateTime ExecutionDate {get; private set;}
    }

    public enum TransactionKind
    {
        DEPOSIT,
        WITHDRAW,
        TRANSFER
    }
}