using System;

namespace HappyBank.Domain.Model
{
    
    /*
    Table: ACCOUNT
    Colunas:
        ID GUID
        NUMBER INT
        OWNER_ID INT (Relacionado com a tabela User)
    */
    public class Account : Entity
    {
        public int Number {get; set;}
        public User Owner {get; private set;}   
    }
}
