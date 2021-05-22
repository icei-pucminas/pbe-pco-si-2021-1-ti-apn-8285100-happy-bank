using System;

namespace HappyBank.UseCases.ExtractStatement
{
    public class ExtractStatementInput
    {
        public DateTime Start {get; set;}
        public DateTime End {get; set;}
        public Guid CustomerId {get; set;}
    }
}