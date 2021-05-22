using System;

namespace HappyBank.Domain.Model
{
    public class ExtractStatement
    {
        public Guid Id {get; set;}
        public string Description {get; set;}
        public DateTime ExecutionDate {get; set;}
        public decimal Value {get; set;}
    }
}