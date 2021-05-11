using System;

namespace HappyBank.Domain.Model
{
    public class Transfer : Transaction
    {
        protected Transfer(Account account, Account accountDestiny, decimal value, DateTime executionDate) 
            : base(account, value, executionDate, TransactionKind.TRANSFER)
        {
            this.Id = Guid.NewGuid();
            this.AccountDestiny = accountDestiny;   
        }
        public Account AccountDestiny {get; private set;}
    }
}