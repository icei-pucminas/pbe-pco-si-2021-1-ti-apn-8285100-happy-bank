using System;

namespace HappyBank.UseCases.DoDeposit
{
    public class DoDepositOutput
    {
        public String CustomerName {get; set;}
        public Guid TransactionId {get; set;}
    }
}