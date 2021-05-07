using System;

namespace HappyBank.Domain.Model
{
    public class Customer : Entity
    {
        public Customer(string name, string govNumer, string street, string district, string city, string state, string addressNumber, DateTime birthDate, string phone, string email, string password)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.GovNumber = govNumer;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.Street = state;
            this.AddressNumber = addressNumber;
            this.BirthDate = birthDate;
            this.Phone = phone;
            this.Email = email;
            this.Password = password;
        }
        
        public string Name {get; private set;}
        public string GovNumber {get; private set;}
        public string Street {get; private set;}
        public string District {get; private set;}
        public string City {get; private set;}
        public string State {get; private set;}
        public string AddressNumber {get; private set;}
        public DateTime BirthDate {get; private set;}
        public string Phone {get; private set;}
        public string Email {get; private set;}
        public string Password {get; private set;}
    }
}
