using Allsop.Domain.Contract.Queries;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class GetShoppingCartByUserIdHandler: IRequestHandler<GetShoppingCartByUserIdQuery, ShoppingCart>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public GetShoppingCartByUserIdHandler(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public Task<ShoppingCart> Handle(GetShoppingCartByUserIdQuery request, CancellationToken cancellationToken)
            => _shoppingCartService.GetShoppingCartByUserIdAsync(request.UserId);
    }
}
