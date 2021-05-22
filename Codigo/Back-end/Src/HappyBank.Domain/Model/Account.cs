using System;

namespace HappyBank.Domain.Model
{
    public class Account : Entity
    {
        public Account(Guid bankId, Guid customerId, int agencyNumber)
        {
            this.Id = Guid.NewGuid();
            this.BankId = bankId;
            this.CustomerId = customerId;
            this.AgencyNumber = agencyNumber;
        }

        public Account(Guid id, Guid bankId, Guid customerId, int agencyNumber, int accountNumber)
        {
            this.Id = id;
            this.BankId = bankId;
            this.CustomerId = customerId;
            this.AgencyNumber = agencyNumber;
            this.AccountNumber = accountNumber;
        }

        public Guid BankId {get; set;}
        public Guid CustomerId {get; set;}
        public int AgencyNumber {get; set;}
        public int AccountNumber {get; set;}
    }
}
