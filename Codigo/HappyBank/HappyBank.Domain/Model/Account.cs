using System;

namespace HappyBank.Domain.Model
{
    public class Account : Entity
    {
        public int Number {get; set;}
        public User Owner {get; private set;}   
    }
}