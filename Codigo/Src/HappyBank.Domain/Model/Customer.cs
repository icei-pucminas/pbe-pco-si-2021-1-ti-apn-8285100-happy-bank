using System;

namespace HappyBank.Domain.Model
{
    public class Customer : Entity
    {
        public Customer(Guid id, string name, string govNumber, string street, string district, string city, string state, string addressNumber, DateTime birthDate, string phone, string email, string password)
        {
            this.Id = id;
            this.Name = name;
            this.GovNumber = govNumber;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.State = state;
            this.AddressNumber = addressNumber;
            this.BirthDate = birthDate;
            this.Phone = phone;
            this.Email = email;
            this.Password = password;
        }
        
        public Customer(string name, string govNumber, string street, string district, string city, string state, string addressNumber, DateTime birthDate, string phone, string email, string password)
        {
            this.Name = name;
            this.GovNumber = govNumber;
            this.Street = street;
            this.District = district;
            this.City = city;
            this.State = state;
            this.AddressNumber = addressNumber;
            this.BirthDate = birthDate;
            this.Phone = phone;
            this.Email = email;
            this.Password = password;
        }
        
        public string Name {get; set;}
        public string GovNumber {get; set;}
        public string Street {get; set;}
        public string District {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string AddressNumber {get; set;}
        public DateTime BirthDate {get; set;}
        public string Phone {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
    }
}
