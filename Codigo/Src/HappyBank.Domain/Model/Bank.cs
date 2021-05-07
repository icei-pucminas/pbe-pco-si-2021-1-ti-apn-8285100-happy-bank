using System;

namespace HappyBank.Domain.Model
{
    public class Bank : Entity
    {
        public Bank(int bankNumber, string name, string govNumber, string street, string district, string city, string state, string addressNumber)
        {
            this.Id = Guid.NewGuid();
            this.BankNumber = bankNumber;
            this.Name = name;
            this.GovNumber = govNumber;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.State = state;
            this.AddressNumber = this.AddressNumber;
        }

        public int BankNumber {get; private set;}
        public string Name {get; private set;}
        public string GovNumber {get; private set;}
        public string Street {get; private set;}
        public string District {get; private set;}
        public string City {get; private set;}
        public string State {get; private set;}
        public string AddressNumber {get; private set;}
    }
}