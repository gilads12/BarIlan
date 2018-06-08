namespace Calculator.Core.Exceptions
{
    public class NotValidOperationException : GlobalException
    {
        public string Operation { get; set; }
        public override string ErrorMessage => $"Invalid operation request. Operation : {Operation} can't be calculate!";

        public NotValidOperationException(string operation)
        {
            Operation = operation;
        }

    }
}
