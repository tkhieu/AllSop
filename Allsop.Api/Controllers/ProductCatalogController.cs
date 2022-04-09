using Allsop.Common.Mediator;
using Allsop.Domain.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Allsop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogController : ControllerBase
    {
        private readonly ILogger<ProductCatalogController> _logger;
        private readonly IScopedMediator _scopedMediator;

        public ProductCatalogController(ILogger<ProductCatalogController> logger, IScopedMediator scopedMediator)
        {
            _logger = logger;
            _scopedMediator = scopedMediator;
        }


        [HttpGet("Products")]
        public async Task<ActionResult> Products()
        {
            var result = await _scopedMediator.Send(new GetAllProductsQuery());

            return Ok(result);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult> Categories()
        {
            var result = await _scopedMediator.Send(new GetAllCategoriesQuerys());

            return Ok(result);
        }
    }
}
