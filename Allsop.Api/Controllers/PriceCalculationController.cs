using Allsop.Common.Mediator;
using Allsop.Domain.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Allsop.Api.Controllers
{
    namespace Allsop.Api.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class PriceCalculationController : ControllerBase
        {
            private readonly ILogger<PriceCalculationController> _logger;
            private readonly IScopedMediator _scopedMediator;

            public PriceCalculationController(ILogger<PriceCalculationController> logger, IScopedMediator scopedMediator)
            {
                _logger = logger;
                _scopedMediator = scopedMediator;
            }

            [HttpGet("Promotions")]
            public async Task<ActionResult> Promotions()
            {
                var result = await _scopedMediator.Send(new GetAllPromotionsQuery());

                return Ok(result);
            }
        }
    }

}
