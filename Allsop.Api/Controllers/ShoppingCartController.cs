using Allsop.Common.Mediator;
using Allsop.Domain.Contract.Mutations;
using Allsop.Domain.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Allsop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IScopedMediator _scopedMediator;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IScopedMediator scopedMediator)
        {
            _logger = logger;
            _scopedMediator = scopedMediator;
        }


        [HttpGet("ShoppingCart")]
        public async Task<ActionResult> ShoppingCart([FromQuery] Guid userId)
        {
            var result = await _scopedMediator.Send(new GetShoppingCartByUserIdQuery(userId));

            return Ok(result);
        }

        [HttpGet("ShoppingCarts")]
        public async Task<ActionResult> ShoppingCarts()
        {
            var result = await _scopedMediator.Send(new GetAllShoppingCartsQuery());

            return Ok(result);
        }


        [HttpPost("AddProductToShoppingCart")]
        public async Task<ActionResult> AddProductToShoppingCart([FromQuery] Guid productId, [FromQuery] byte quantity, [FromQuery] Guid userId)
        {
            var result = await _scopedMediator.Send(new AddProductToShoppingCartMutation(productId, quantity, userId));

            return Ok(result);
        }

        [HttpPost("RemoveProductOutOfShoppingCart")]
        public async Task<ActionResult> RemoveProductOutOfShoppingCart([FromQuery] Guid productId, [FromQuery] byte quantity, [FromQuery] Guid userId)
        {
            var result = await _scopedMediator.Send(new RemoveProductOutOfShoppingCartMutation(productId, quantity, userId));

            return Ok(result);
        }

        [HttpPost("ApplyVoucherToShoppingCart")]
        public async Task<ActionResult> ApplyVoucherToShoppingCart([FromQuery] string voucher, [FromQuery] Guid userId)
        {
            var result = await _scopedMediator.Send(new ApplyVoucherToShoppingCartMutation(voucher, userId));

            return Ok(result);
        }
    }
}