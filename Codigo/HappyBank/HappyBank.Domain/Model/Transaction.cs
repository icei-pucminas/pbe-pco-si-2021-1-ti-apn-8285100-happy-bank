using System;

namespace HappyBank.Domain.Model
{
     /*
    Table: TRANSACTION
    Colunas:
        ID GUID
        ACCOUNT_ID GUID (Relacionado com a tabela ACCOUNT)
        KIND CHAR(1) (D, W, T)            
        VALUE DECIMAL
        EXECUTION_DATE DATE_TIME
    */
    public abstract class Transaction : Entity
    {
        protected Transaction(Account account, decimal value, DateTime executionTime, TransactionKind kind)
        {
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