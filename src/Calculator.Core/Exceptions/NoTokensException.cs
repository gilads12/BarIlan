using System;

namespace Calculator.Core.Exceptions
{
    public class NoTokensException : GlobalException
    {
        public override string ErrorMessage => "Get no tokens error!";

        public NoTokensException()
        {
        }
    }
}
