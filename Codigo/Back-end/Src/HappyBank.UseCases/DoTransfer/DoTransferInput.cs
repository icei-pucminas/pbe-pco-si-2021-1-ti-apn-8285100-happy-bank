using System;

namespace HappyBank.UseCases.DoTransfer
{
    public class DoTransferInput
    {
        public Guid CustomerId { get; set; }
        public decimal Value { get; set; }
        public Guid AccountDestinyId { get; set; }
    }
}