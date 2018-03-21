using System;

namespace Calculator.Core.Exceptions
{
    public class NotValidOperationException : Exception
    {
        public NotValidOperationException()
        {
        }

        public NotValidOperationException(string message) : base(message)
        {
        }

        public NotValidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
