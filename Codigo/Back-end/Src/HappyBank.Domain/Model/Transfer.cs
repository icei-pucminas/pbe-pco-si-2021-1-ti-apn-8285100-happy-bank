using System;

namespace HappyBank.Domain.Model
{
    public class Transfer : Transaction
    {
        public Transfer(Guid id,Guid accountId, Guid accountDestinyId, decimal value, DateTime executionDate)
            : base(accountId, value, executionDate, TransactionKind.TRANSFER)
        {
            this.Id = id;
            this.AccountDestinyId = accountDestinyId;
        }
        public Transfer(Guid accountId, Guid accountDestinyId, decimal value, DateTime executionDate)
            : base(accountId, value, executionDate, TransactionKind.TRANSFER)
        {
            this.AccountDestinyId = accountDestinyId;
        }

        public Guid AccountDestinyId { get; set; }
    }
}