using System;

namespace HappyBank.Domain.Model
{
    public class Account : Entity
    {
        public Account(Guid bankId, Guid customerId)
        {
            this.Id = Guid.NewGuid();
            this.BankId = bankId;
            this.CustomerId = customerId;
        }

        public Account(Guid id, Guid bankId, Guid customerId, int agencyNumber, int accountNumber)
        {
            this.Id = id;
            this.BankId = bankId;
            this.CustomerId = customerId;
            this.AgencyNumber = agencyNumber;
            this.AccountNumber = accountNumber;
        }

        public Guid BankId {get; private set;}
        public Guid CustomerId {get; private set;}
        public int AgencyNumber {get; private set;}
        public int AccountNumber {get; private set;}
    }
}
