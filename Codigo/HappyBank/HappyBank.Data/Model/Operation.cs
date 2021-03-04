namespace HappyBank.Data.Model
{
    public class Operation
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