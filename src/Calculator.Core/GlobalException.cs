using System;

namespace Calculator.Core.Exceptions
{
    public abstract class GlobalException : Exception
    {
        public override string Message => ErrorMessage;
        public abstract string ErrorMessage { get; }

    }
}
