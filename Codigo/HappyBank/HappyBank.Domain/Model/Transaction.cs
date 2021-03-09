namespace HappyBank.Domain.Model
{
    public class Transaction : Entity
    {
        protected Transaction(Account account, decimal value, TransactionKind kind)
        {
            this.Account = account;
            this.Value = value;
            this.Kind = kind;
        }
        public Account Account {get; private set;}
        public TransactionKind Kind {get; private set;}
        public decimal Value {get; private set;}
    }

    public enum TransactionKind
    {
        DEPOSIT,
        WITHDRAW,
        TRANSFER
    }
}