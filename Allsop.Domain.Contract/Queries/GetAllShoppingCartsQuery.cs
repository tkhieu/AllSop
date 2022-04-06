using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Contract.Queries
{
    public class GetAllShoppingCartsQuery: IRequest<IEnumerable<ShoppingCart>>
    {
    }
}
