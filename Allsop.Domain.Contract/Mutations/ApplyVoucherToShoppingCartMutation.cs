using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Contract.Mutations
{
    public class ApplyVoucherToShoppingCartMutation : IRequest<ShoppingCart>
    {
        public string Voucher { get; set; }
        public Guid UserId { get; set; }

        public ApplyVoucherToShoppingCartMutation(string voucher, Guid userId)
        {
            Voucher = voucher;
            UserId = userId;
        }
    }
}
