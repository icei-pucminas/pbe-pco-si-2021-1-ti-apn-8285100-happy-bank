using System;

namespace HappyBank.Domain.Model
{
    public class Operation : Entity
    {
        public Guid AccountId {get; private set;}
        public decimal Value {get; private set;}
        public OperationKind Kind {get; private set;}
        public Transaction Transaction {get; private set;}
    }

    public enum OperationKind
    {
        CREDIT,
        DEBIT
    }
}