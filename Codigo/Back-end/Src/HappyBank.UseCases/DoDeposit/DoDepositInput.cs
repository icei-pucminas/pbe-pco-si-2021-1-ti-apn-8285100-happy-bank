namespace HappyBank.UseCases.DoDeposit
{
    public class DoDepositInput
    {
        public int AgencyNumber { get; set; }
        public int AccountNumber { get; set; }
        public string EnvelopeCode { get; set; }
        public decimal Value { get; set; }
    }
}