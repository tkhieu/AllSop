using Allsop.Domain.Contract.Queries;
using MediatR;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;

namespace Allsop.Domain.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductService _productService;

        public GetAllProductsHandler(IProductService productService)
        {
            _productService = productService;
        }
        public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            => _productService.GetAllProductsAsync();
    }
}
