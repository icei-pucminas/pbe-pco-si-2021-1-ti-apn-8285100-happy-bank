using System;

namespace HappyBank.UseCases.DoWithdraw
{
    public class DoWithdrawInput
    {
        public Guid CustomerId { get; set; }
        public decimal Value {get; set;}
        public string TerminalCode {get; set;}
    }
}