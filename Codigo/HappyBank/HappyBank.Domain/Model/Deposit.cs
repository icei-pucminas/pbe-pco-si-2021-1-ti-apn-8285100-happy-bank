using System;

namespace HappyBank.Domain.Model
{
     /*
    Table: DEPOSIT
    Colunas:
        ID GUID (Relacionado com a tabela TRANSACTION usando o mesmo ID)
        ENVELOP_CODE VARCHAR(100)
    */
    public class Deposit : Transaction
    {
        public Deposit(Account account, decimal value, DateTime executionTime, string envelopeCode)
            : base (account, value, executionTime, TransactionKind.DEPOSIT)
        {
            this.EnvelopeCode = envelopeCode;
        }
        public string EnvelopeCode {get; private set;}
    }
}