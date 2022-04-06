using Allsop.Domain.Contract.Mutations;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class RemoveProductOutOfShoppingHandler : IRequestHandler<RemoveProductOutOfShoppingCartMutation, ShoppingCart>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public RemoveProductOutOfShoppingHandler(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public Task<ShoppingCart> Handle(RemoveProductOutOfShoppingCartMutation request, CancellationToken cancellationToken)
            => _shoppingCartService.RemoveProductAsync(request.ProductId, request.Quantity, request.UserId);
    }
}
