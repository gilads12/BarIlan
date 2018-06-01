namespace Calculator.Core.Exceptions
{
    public class NotValidTokenException : GlobalException
    {
        public string Token { get; set; }
        public override string ErrorMessage => $"{Token} is not valid token!";

        public NotValidTokenException(string token)
        {
            Token = token;
        }

    }
}
