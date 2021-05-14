using System;

namespace HappyBank.Domain.Model
{
    public class Employee : Entity
    {
        public Employee(Guid id, string registration, Bank bank, decimal wage, string name, string function)
        {
            this.Id = id;
            this.Registration = registration;
            this.Bank = bank;
            this.Wage = wage;
            this.Name = name;
            this.Function = function;
        }

        public Employee(string registration, Bank bank, decimal wage, string name, string function)
        {
            this.Registration = registration;
            this.Bank = bank;
            this.Wage = wage;
            this.Name = name;
            this.Function = function;
        }

        public string Registration { get; set; }
        public Bank Bank { get; set; }
        public decimal Wage { get; set; }
        public string Name { get; set; }
        public string Function { get; set; }
    }
}