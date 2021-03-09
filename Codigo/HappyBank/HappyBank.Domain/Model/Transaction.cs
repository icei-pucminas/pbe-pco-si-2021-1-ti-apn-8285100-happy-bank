namespace HappyBank.Domain.Model
{
     /*
    Table: TRANSACTION
    Colunas:
        ID GUID
        ACCOUNT_ID GUID (Relacionado com a tabela ACCOUNT)
        KIND CHAR(1) (D, W, T)            
        VALUE DECIMAL            
    */
    public abstract class Transaction : Entity
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