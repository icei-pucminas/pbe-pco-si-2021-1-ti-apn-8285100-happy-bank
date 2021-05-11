using System;

namespace HappyBank.UseCases.CustomerRegistration
{
    public class CustomerRegistrationInput
    {
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