using System;

namespace HappyBank.UseCases.Exceptions
{
    public class CustomerDuplicatedException : Exception
    {
        public CustomerDuplicatedException() : base() { }
        public CustomerDuplicatedException(string message) : base(message) { }
        public CustomerDuplicatedException(string message, Exception inner) : base(message, inner) { }
        
        protected CustomerDuplicatedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}