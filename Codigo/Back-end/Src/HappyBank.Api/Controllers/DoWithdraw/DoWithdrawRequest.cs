using System;

namespace HappyBank.Api.Controllers.DoWithdraw
{
    public class DoWithdrawRequest
    {
        public decimal Value { get; set; }
        public string TerminalCode { get; set; }
    }
}