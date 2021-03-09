namespace HappyBank.Domain.Model
{
    public class Transfer : Transaction
    {
        protected Transfer(Account account, Account accountDestiny, decimal value) : base(account, value, TransactionKind.TRANSFER)
        {
            this.AccountDestiny = accountDestiny;   
        }
        public Account AccountDestiny {get; private set;}
    }
}