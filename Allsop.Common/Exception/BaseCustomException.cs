using System.Net;

namespace Allsop.Common.Exception
{
    public class BaseCustomException : System.Exception
    {
        public int StatusCode { get; }

        public IEnumerable<string> DetailedMessages { get; }

        public Guid Id { get; }

        public ErrorCode ErrorCode { get; }

        public BaseCustomException(string message, IEnumerable<string> detailedMessages, int statusCode, ErrorCode errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            DetailedMessages = detailedMessages;
            Id = Guid.NewGuid();
        }

        public BaseCustomException(string message, IEnumerable<string> detailedMessages, ErrorCode errorCode)
            : this(message, detailedMessages, (int)HttpStatusCode.BadRequest, errorCode)
        {
        }
    }
}
