using Allsop.Domain.Contract.Queries;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuerys, IEnumerable<Category>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<IEnumerable<Category>> Handle(GetAllCategoriesQuerys request, CancellationToken cancellationToken)
            => _categoryService.GetAllCategoriesAsync();
    }
}
