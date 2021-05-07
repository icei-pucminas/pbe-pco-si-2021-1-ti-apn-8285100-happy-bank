using System;

namespace HappyBank.Domain.Model
{
    public class Account : Entity
    {
        public Account(Bank Bank, Customer customer)
        {
            this.Id = Guid.NewGuid();
            this.Bank = Bank;
            this.Customer = customer;
        }

        public Bank Bank {get; private set;}
        public Customer Customer {get; private set;}
        public int AgencyNumber {get; private set;}
        public int AccountNumber {get; private set;}
    }
}
