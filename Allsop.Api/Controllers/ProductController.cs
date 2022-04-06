using Allsop.Common.Mediator;
using Allsop.Domain.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Allsop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IScopedMediator _scopedMediator;

        public ProductController(ILogger<ProductController> logger, IScopedMediator scopedMediator)
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

        [HttpGet("Promotions")]
        public async Task<ActionResult> Promotions()
        {
            var result = await _scopedMediator.Send(new GetAllPromotionsQuery());

            return Ok(result);
        }
    }
}
