namespace Allsop.Common.Exception
{
    public class InvalidVoucherException : BaseCustomException
    {
        public InvalidVoucherException(string voucher)
            : base($"Voucher '{voucher}' does not exist.", Array.Empty<string>(), ErrorCode.InvalidId) { }
    }
}
