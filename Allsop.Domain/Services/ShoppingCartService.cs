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

        public  Task<ShoppingCart> GetShoppingCartByUserIdAsync(Guid userId)
            => _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);

        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync()
            => _shoppingCartRepository.GetAllShoppingCartsAsync();

        public async Task<ShoppingCart> AddProductAsync(Guid productId, byte quantity, Guid userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            var item = shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                if (item.Product.Quantity < quantity)
                {
                    throw new OutOfStockException(item.Product.Name);
                }

                item.Quantity += quantity;
                item.Product.Quantity -= quantity;
            }
            else
            {
                item = new ShoppingCartItem()
                {
                    Id = Guid.NewGuid(),
                    Quantity = quantity,
                    ProductId = productId,
                    Product = await _productService.GetProductAsync(productId),
                    ShoppingCartId = shoppingCart.Id
                };

                shoppingCart.ShoppingCartItems.Add(item);
            }

            shoppingCart.DiscountAmount = await _promotionService.GetTotalDiscountAmount(shoppingCart);

            await _shoppingCartItemService.CreateOrUpdateShoppingCartItemAsync(item);
            await _productService.IncreaseQuantity(productId, quantity);
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }

        public async Task<ShoppingCart> RemoveProductAsync(Guid productId, byte quantity, Guid userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            var item = shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);

            if (item == null)
            {
                return shoppingCart;
            }

            if (item.Quantity <= quantity)
            {
                shoppingCart.ShoppingCartItems.Remove(item);
            }
            else
            {
                item.Quantity -= quantity;
                item.Product.Quantity += quantity;
            }

            shoppingCart.DiscountAmount = await _promotionService.GetTotalDiscountAmount(shoppingCart);

            await _productService.DecreaseQuantity(productId, quantity);
            await _shoppingCartItemService.UpdateShoppingCartItemAsync(item);
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }


        public async Task<ShoppingCart> ApplyVoucherAsync(string voucher, Guid userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            var promotions = await _promotionService.GetAllPromotionsAsync();
            var promotion = promotions.FirstOrDefault(x =>
                string.Equals(x.Voucher, voucher, StringComparison.InvariantCultureIgnoreCase));

            if (promotion == null)
            {
                throw new VoucherNotFoundException(voucher);
            }

            shoppingCart.Voucher = voucher;
            shoppingCart.DiscountAmount = await _promotionService.GetDiscountAmountByVoucher(shoppingCart, promotion);

            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;
        }
    }
}
