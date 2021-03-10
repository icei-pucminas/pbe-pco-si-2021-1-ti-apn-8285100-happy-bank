using System;

namespace HappyBank.UseCases.Exceptions
{
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException() : base() { }
        public InvalidUsernameException(string message) : base(message) { }
        public InvalidUsernameException(string message, Exception inner) : base(message, inner) { }
        
        protected InvalidUsernameException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}