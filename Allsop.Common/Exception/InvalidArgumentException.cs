namespace Allsop.Common.Exception
{
    public class InvalidArgumentException : BaseCustomException
    {
        public string Argument { get; }

        public InvalidArgumentException(string argument, string message)
            : base($"Invalid argument '{argument}': {message}", Array.Empty<string>(), ErrorCode.InvalidArgument)
        {
            Argument = argument;
        }
    }
}
