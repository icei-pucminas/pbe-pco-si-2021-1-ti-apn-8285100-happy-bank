using System;

namespace HappyBank.UseCases.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base() { }
        public AccountNotFoundException(string message) : base(message) { }
        public AccountNotFoundException(string message, Exception inner) : base(message, inner) { }
        
        protected AccountNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}