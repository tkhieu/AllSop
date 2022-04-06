namespace Allsop.Common.Exception
{
    public class VoucherNotFoundException : BaseCustomException
    {
        public VoucherNotFoundException(string voucher)
            : base($"Voucher '{voucher}' does not exist.", Array.Empty<string>(), ErrorCode.InvalidId) { }
    }
}
