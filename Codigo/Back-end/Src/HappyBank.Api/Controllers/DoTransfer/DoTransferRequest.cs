using System;

namespace HappyBank.Api.Controllers.DoTransfer
{
    public class DoTransferRequest
    {
        public decimal Value { get; set; }
        public Guid AccountDestinyId { get; set; }
    }
}