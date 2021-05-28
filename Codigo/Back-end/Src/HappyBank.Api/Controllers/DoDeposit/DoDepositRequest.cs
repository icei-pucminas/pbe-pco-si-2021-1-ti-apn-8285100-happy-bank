namespace HappyBank.Api.Controllers.SignIn
{
    public class DoDepositRequest
    {
        public int AgencyNumber { get; set; }
        public int AccountNumber { get; set; }
        public string EnvelopeCode { get; set; }
        public decimal Value { get; set; }
    }
}