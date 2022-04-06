using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class ShoppingCart:BaseModel
    {
        public Guid UserId { get; set; }
        public string? Voucher { get; set; }
        public decimal DiscountAmount { get; set; }

        public int TotalItems
        {
            get
            {
                return ShoppingCartItems.Sum(x => x.Quantity);

            }
        }

        public decimal SubTotal {
            get
            {
                return ShoppingCartItems.Sum(x => x.Amount);
            }
        }
       
        public decimal Total {
            get
            {
                return SubTotal - DiscountAmount;
            }
        }
        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
