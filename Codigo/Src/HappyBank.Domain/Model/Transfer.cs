using System;

namespace HappyBank.Domain.Model
{
    /*
    Table: TRANSFER
    Colunas:
        ID GUID (Relacionado com a tabela TRANSACTION usando o mesmo ID)
        ACCOUNT_DESTINY GUID (Relacionado com a tabela ACCOUNT)
    */
    public class Transfer : Transaction
    {
        protected Transfer(Account account, Account accountDestiny, decimal value, DateTime executionDate) 
            : base(account, value, executionDate, TransactionKind.TRANSFER)
        {
            this.AccountDestiny = accountDestiny;   
        }
        public Account AccountDestiny {get; private set;}
    }
}