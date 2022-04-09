using Allsop.Common.Enums;
using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Repository;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;

namespace Allsop.Domain.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IPromotionService _promotionService;
        private readonly IProductService _productService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemService shoppingCartItemService, IPromotionService promotionService, IProductService productService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemService = shoppingCartItemService;
            _promotionService = promotionService;
            _productService = productService;
        }

        public Task<ShoppingCart> GetShoppingCartByUserIdAsync(Guid userId)
            => _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);

        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync()
            => _shoppingCartRepository.GetAllShoppingCartsAsync();

        public async Task<ShoppingCart> AddProductAsync(Guid productId, byte quantity, Guid userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            var item = shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
            var product = await _productService.GetProductAsync(productId);

            if (item != null)
            {
                if (product.Quantity < quantity)
                {
                    throw new OutOfStockException(item.ProductId.ToString());
                }

                item.Quantity += quantity;
            }
            else
            {
                item = new ShoppingCartItem()
                {
                    Id = Guid.NewGuid(),
                    Quantity = quantity,
                    ProductId = productId,
                    ShoppingCartId = shoppingCart.Id
                };

                shoppingCart.ShoppingCartItems.Add(item);
            }

            shoppingCart.DiscountAmount = await GetTotalDiscountAmount(shoppingCart);

            await _shoppingCartItemService.CreateOrUpdateShoppingCartItemAsync(item);
            await _productService.IncreaseQuantity(productId, quantity);
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }

        public async Task<ShoppingCart> RemoveProductAsync(Guid productId, byte quantity, Guid userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            var item = shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
            var decreasedQuantity = 0;

            if (item == null)
            {
                return shoppingCart;
            }

            if (item.Quantity <= quantity)
            {
                shoppingCart.ShoppingCartItems.Remove(item);
                decreasedQuantity = item.Quantity;
            }
            else
            {
                item.Quantity -= quantity;
                decreasedQuantity = quantity;
            }

            shoppingCart.DiscountAmount = await GetTotalDiscountAmount(shoppingCart);

            await _productService.DecreaseQuantity(productId, decreasedQuantity);
            await _shoppingCartItemService.UpdateShoppingCartItemAsync(item);
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }

        public async Task<ShoppingCart> ApplyVoucherAsync(string voucher, Guid userId)
        {
            await _promotionService.CheckValidVoucherAsync(voucher);

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);

            shoppingCart.Voucher = voucher;
            shoppingCart.DiscountAmount = await GetTotalDiscountAmount(shoppingCart);

            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }

        private async Task<decimal> GetTotalDiscountAmount(ShoppingCart shoppingCart)
        {
            var totalDiscountAmount = 0m;
            var promotions = await _promotionService.GetAllPromotionsAsync();

            foreach (var promotion in promotions)
            {
                _ = promotion.PromotionType switch
                {
                    PromotionType.Amount => totalDiscountAmount += await GetDiscountAmountByAmount(shoppingCart, promotion),
                    PromotionType.Percent => totalDiscountAmount += await GetDiscountAmountByPercent(shoppingCart, promotion),
                    _ => totalDiscountAmount += await _promotionService.GetDiscountAmountByVoucher(shoppingCart.Voucher, shoppingCart.SubTotal, promotion)
                };
            };

            return totalDiscountAmount;
        }

        private async Task<decimal> GetDiscountAmountByAmount(ShoppingCart shoppingCart, Promotion promotion)
        {
            var items = shoppingCart.ShoppingCartItems.Where(x => x.CategoryId == promotion.CategoryId);
            var discountAmount = 0m;

            if (items.Sum(x => x.Amount) >= promotion.SpendAmount)
            {

                return promotion.DiscountAmount;
            }

            return discountAmount;
        }

        private async Task<decimal> GetDiscountAmountByPercent(ShoppingCart shoppingCart, Promotion promotion)
        {
            var items = shoppingCart.ShoppingCartItems.Where(x => x.CategoryId == promotion.CategoryId);
            var discountAmount = 0m;

            if (items.Sum(x => x.Quantity) >= promotion.SpendQuantity)
            {

                foreach (var shoppingCartItem in items)
                {
                    discountAmount += (shoppingCartItem.Quantity * shoppingCartItem.Price * promotion.DiscountPercent) / 100;
                }
            }

            return discountAmount;
        }
    }
}
