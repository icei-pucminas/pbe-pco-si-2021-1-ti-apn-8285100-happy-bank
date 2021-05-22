using System;
using System.Collections.Generic;

namespace HappyBank.UseCases.ExtractStatement
{
    public class ExtractStatementOutput : List<ExtractStatementItem>
    {
           
    }
    
    public class ExtractStatementItem 
    {
        public Guid Id {get; set;}
        public DateTime ExecutionDate {get; set;}
        public string Description {get; set;}
        public decimal Value {get; set;}
    }
}