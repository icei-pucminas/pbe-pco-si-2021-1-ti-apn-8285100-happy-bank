namespace HappyBank.Domain.Model
{
    public class Withdraw : Transaction
    {
        public Withdraw(Account account, decimal value, string terminalCode) : base (account, value, TransactionKind.WITHDRAW)
        {
            this.TerminalCode = terminalCode;
        }
        public string TerminalCode {get; private set;}
    }
}