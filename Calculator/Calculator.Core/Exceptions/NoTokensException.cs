using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Calculator.Core.Exceptions
{
    public class NoTokensException : Exception
    {
        public NoTokensException()
        {
        }

        public NoTokensException(string message) : base(message)
        {
        }

        public NoTokensException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
