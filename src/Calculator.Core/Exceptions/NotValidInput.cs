namespace Calculator.Core.Exceptions
{

    public class NotValidInput : GlobalException
    {
        public string Input { get; set; }
        public override string ErrorMessage => $"Invalid json request. Input : {Input} in worng format!";

        public NotValidInput(string input)
        {
            Input = input;
        }

    }
}
