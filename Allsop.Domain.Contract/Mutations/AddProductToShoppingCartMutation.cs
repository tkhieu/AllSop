using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Contract.Mutations
{
    public class AddProductToShoppingCartMutation : IRequest<ShoppingCart>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public byte Quantity { get; set; }

        public AddProductToShoppingCartMutation(Guid productId, byte quantity, Guid userId)
        {
            ProductId = productId;
            UserId = userId;
            Quantity = quantity;
        }
    }
}
