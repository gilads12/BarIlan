using System;

namespace Calculator.Core.Exceptions
{
    public abstract class GlobalException : Exception
    {
        public abstract string ErrorMessage { get; }

    }
}
