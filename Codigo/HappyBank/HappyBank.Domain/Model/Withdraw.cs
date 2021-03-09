namespace HappyBank.Domain.Model
{
    /*
    Table: TRANSFER
    Colunas:
        ID GUID (Relacionado com a tabela TRANSACTION usando o mesmo ID)
        TERMINAL_CODE VARCHAR(100)
    */
    public class Withdraw : Transaction
    {
        public Withdraw(Account account, decimal value, string terminalCode) : base (account, value, TransactionKind.WITHDRAW)
        {
            this.TerminalCode = terminalCode;
        }
        public string TerminalCode {get; private set;}
    }
}