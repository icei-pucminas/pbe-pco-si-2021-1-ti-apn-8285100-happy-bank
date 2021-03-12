using System;

namespace HappyBank.Domain.Model
{
    
    /*
    Table: ACCOUNT
    Colunas:
        ID GUID
        ACCOUNT_NUMBER INT
        BRANCH_NUMBER INT
        OWNER_ID INT (Relacionado com a tabela User)
    */
    public class Account : Entity
    {
        public Account(User owner, int branchNumber)
        {
            this.Id = Guid.NewGuid();
            this.Owner = owner;
            this.BranchNumber = branchNumber;
            this.AccountNumber = new Random().Next(9999); //TODO: Verificar como gerar número da conta. Talvez uma sequence no banco de dados
        }

        public int AccountNumber {get; set;}
        public int BranchNumber {get; set;}
        public User Owner {get; private set;}   
    }
}
