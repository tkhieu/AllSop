using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class ShoppingCartDb: BaseEntityDb
    {
        public Guid UserId { get; set; }
        public string? Voucher { get;set; }
        public int TotalItems { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total
        {
            get
            {
                return TotalAmount - DiscountAmount;
            }
        }
        public IList<ShoppingCartItemDb> ShoppingCartItems { get; set; }
    }
}
