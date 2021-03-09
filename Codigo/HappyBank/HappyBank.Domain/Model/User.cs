using System;

namespace HappyBank.Domain.Model
{
    public class User : Entity
    {
        public string Name {get; private set;}
        public string Username {get; private set;}
    }
}
