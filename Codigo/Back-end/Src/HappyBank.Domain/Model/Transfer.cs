using System;

namespace HappyBank.Domain.Model
{
    public class Transfer : Transaction
    {
        protected Transfer(Guid accountId, Guid accountDestinyId, decimal value, DateTime executionDate) 
            : base(accountId, value, executionDate, TransactionKind.TRANSFER)
        {
            this.Id = Guid.NewGuid();
            this.AccountDestinyId = accountDestinyId;   
        }
        public Guid AccountDestinyId {get; private set;}
    }
}