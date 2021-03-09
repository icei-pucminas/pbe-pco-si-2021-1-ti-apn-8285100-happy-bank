using System;


/*
    Table: USER
    Colunas:
        ID GUID
        NAME VARCHAR(100)
        USER_NAME INT (Relacionado com a tabela User)
*/
namespace HappyBank.Domain.Model
{
    public class User : Entity
    {
        public string Name {get; private set;}
        public string Username {get; private set;}
    }
}
