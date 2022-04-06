using Allsop.Domain.Contract.Queries;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class GetAllShoppingCartsHandler : IRequestHandler<GetAllShoppingCartsQuery, IEnumerable<ShoppingCart>>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public GetAllShoppingCartsHandler(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public Task<IEnumerable<ShoppingCart>> Handle(GetAllShoppingCartsQuery request, CancellationToken cancellationToken)
            => _shoppingCartService.GetAllShoppingCartsAsync();
    }
}
