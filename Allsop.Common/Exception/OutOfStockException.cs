namespace Allsop.Common.Exception
{
    public class OutOfStockException : BaseCustomException
    {
        public OutOfStockException(string productName)
            : base($"Product '{productName}' is out of stock.", Array.Empty<string>(), ErrorCode.InvalidId) { }
    }
}
