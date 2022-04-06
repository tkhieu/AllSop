using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Contract.Queries
{
    public class GetShoppingCartByUserIdQuery: IRequest<ShoppingCart>
    {
        public Guid UserId { get; set; }

        public GetShoppingCartByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
