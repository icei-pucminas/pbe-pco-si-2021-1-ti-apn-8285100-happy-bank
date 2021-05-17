using System;

namespace HappyBank.Domain.Model
{
    public class Employee : Entity
    {
        public Employee(Guid id, string name, Guid bankId, string registration, decimal wage, string function)
        {
            this.Id = id;
            this.Name = name;
            this.BankId = bankId;
            this.Registration = registration;            
            this.Wage = wage;            
            this.Function = function;
        }

        public Employee(string name, Guid bankId, string registration, decimal wage, string function)
        {
            this.Name = name;
            this.BankId = bankId;
            this.Registration = registration;            
            this.Wage = wage;            
            this.Function = function;
        }

        public string Registration { get; set; }
        public Guid BankId { get; set; }
        public decimal Wage { get; set; }
        public string Name { get; set; }
        public string Function { get; set; }
    }
}