namespace HappyBank.Domain.Model
{
    /*
    Table: OPERATION
    Colunas:
        ID GUID
        ACCOUNT_ID GUID (Relacionado com a tabela ACCOUNT)
        VALUE DECIMAL
        KIND CHAR(1) (C ou D)
        TRANSACTION_ID GUID (Relacionado com a tabela TRANSACTION)            
    */
    public class Operation : Entity
    {
        public Account Account {get; private set;}
        public decimal Value {get; set;}
        public OperationKind Kind {get; set;}
        public Transaction Transaction {get; set;}
    }

    public enum OperationKind
    {
        CREDIT,
        DEBIT
    }
}