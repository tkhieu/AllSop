using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class ShoppingCartItem:BaseModel
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
