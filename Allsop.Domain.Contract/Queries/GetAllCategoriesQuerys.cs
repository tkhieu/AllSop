using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Contract.Queries
{
    public class GetAllCategoriesQuerys : IRequest<IEnumerable<Category>>
    {
    }
}
