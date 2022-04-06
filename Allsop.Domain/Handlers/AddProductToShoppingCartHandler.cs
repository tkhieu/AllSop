using Allsop.Domain.Contract.Mutations;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class AddProductToShoppingCartHandler : IRequestHandler<AddProductToShoppingCartMutation, ShoppingCart>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public AddProductToShoppingCartHandler(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public Task<ShoppingCart> Handle(AddProductToShoppingCartMutation request, CancellationToken cancellationToken)
            => _shoppingCartService.AddProductAsync(request.ProductId, request.Quantity, request.UserId);
    }
}
