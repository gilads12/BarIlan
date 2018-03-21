using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
