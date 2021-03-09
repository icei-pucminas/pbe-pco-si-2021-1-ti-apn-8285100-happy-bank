namespace HappyBank.Domain.Model
{
    public class Deposit : Transaction
    {
        public Deposit(Account account, decimal value, string envelopeCode) : base (account, value, TransactionKind.DEPOSIT)
        {
            this.EnvelopeCode = envelopeCode;
        }
        public string EnvelopeCode {get; private set;}
    }
}