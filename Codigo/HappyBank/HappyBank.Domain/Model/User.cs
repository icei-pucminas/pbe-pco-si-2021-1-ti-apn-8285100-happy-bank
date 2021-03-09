using System;

namespace HappyBank.Domain.Model
{
    /*
    Table: USER
    Colunas:
        ID GUID
        NAME VARCHAR(100)
        USER_NAME INT (Relacionado com a tabela User)
    */
    public class User : Entity
    {
        public string Name {get; private set;}
        public string Username {get; private set;}
    }
}
