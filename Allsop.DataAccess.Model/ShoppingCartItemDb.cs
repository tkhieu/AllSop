using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class ShoppingCartItemDb : BaseEntityDb
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDb Product { get; set; }
    }
}
