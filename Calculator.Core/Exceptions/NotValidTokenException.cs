using System;

namespace Calculator.Core.Exceptions
{
    public class NotValidTokenException : Exception
    {
        public NotValidTokenException()
        {
        }

        public NotValidTokenException(string message) : base(message)
        {
        }

        public NotValidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
