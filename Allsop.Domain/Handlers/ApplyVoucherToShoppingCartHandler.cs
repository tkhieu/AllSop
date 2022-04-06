using Allsop.Domain.Contract.Mutations;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class ApplyVoucherToShoppingCartHandler : IRequestHandler<ApplyVoucherToShoppingCartMutation, ShoppingCart>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ApplyVoucherToShoppingCartHandler(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public Task<ShoppingCart> Handle(ApplyVoucherToShoppingCartMutation request, CancellationToken cancellationToken)
            => _shoppingCartService.ApplyVoucherAsync(request.Voucher, request.UserId);
    }
}
