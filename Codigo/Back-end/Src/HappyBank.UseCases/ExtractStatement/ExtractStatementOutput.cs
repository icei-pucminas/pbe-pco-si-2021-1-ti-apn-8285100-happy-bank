using System;
using System.Collections.Generic;

namespace HappyBank.UseCases.ExtractStatement
{
    public class ExtractStatementOutput : List<ExtractStatement>
    {
        
    }
    
    public class ExtractStatement 
    {
        public DateTime ExecutionDate {get; set;}
        public string Description {get; set;}
        public decimal Value {get; set;}
    }
}