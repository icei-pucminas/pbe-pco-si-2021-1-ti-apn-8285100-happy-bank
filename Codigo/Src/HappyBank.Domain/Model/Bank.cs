using System;

namespace HappyBank.Domain.Model
{
    public class Bank : Entity
    {
        public Bank(Guid id, int bankNumber, string name, string govNumber, string street, string district, string city, string state, string addressNumber, string v)
        {
            this.Id = id;
            this.BankNumber = bankNumber;
            this.Name = name;
            this.GovNumber = govNumber;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.State = state;
            this.AddressNumber = addressNumber;
        }

        public Bank(int bankNumber, string name, string govNumber, string street, string district, string city, string state, string addressNumber)
        {
            this.BankNumber = bankNumber;
            this.Name = name;
            this.GovNumber = govNumber;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.State = state;
            this.AddressNumber = addressNumber;
        }

        public int BankNumber { get; set; }
        public string Name { get; set; }
        public string GovNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressNumber { get; set; }
    }
}