using Allsop.Common.Cache;
using Allsop.Common.Enums;
using Allsop.DataAccess.Contract.Repository;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Allsop.Domain.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "allPromotions";

        public PromotionService(IPromotionRepository promotionRepository, IMemoryCache cache)
        {
            _promotionRepository = promotionRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            if (_cache.TryGetValue(_cacheKey, out CacheItemModel<IEnumerable<Promotion>> cachedModel))
            {
                return cachedModel.Data;
            }

            var promotions = await _promotionRepository.GetAllPromotionsAsync();

            _cache.Set(_cacheKey, new CacheItemModel<IEnumerable<Promotion>>(promotions));

            return promotions;
        }

        public async Task<decimal> GetTotalDiscountAmount(ShoppingCart shoppingCart)
        {
            var totalDiscountAmount = 0m;
            var promotions = await GetAllPromotionsAsync();

            foreach (var promotion in promotions)
            {
                _ = promotion.PromotionType switch
                {
                    PromotionType.Amount => totalDiscountAmount +=
                        await GetDiscountAmountByAmount(shoppingCart, promotion),
                    PromotionType.Percent => totalDiscountAmount +=
                        await GetDiscountAmountByPercent(shoppingCart, promotion),
                    _ => totalDiscountAmount += await GetDiscountAmountByVoucher(shoppingCart, promotion)
                };
            };

            return totalDiscountAmount;
        }

        public async Task<decimal> GetDiscountAmountByVoucher(ShoppingCart shoppingCart, Promotion promotion)
        {
            if (!string.Equals(shoppingCart.Voucher, promotion.Voucher, StringComparison.InvariantCultureIgnoreCase))
            {
                return 0;
            }

            if (shoppingCart.ShoppingCartItems.Sum(x => x.Amount) >= promotion.SpendAmount)
            {
                return promotion.DiscountAmount;
            }

            return 0;
        }

        public async Task<decimal> GetDiscountAmountByPercent(ShoppingCart shoppingCart, Promotion promotion)
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

        public async Task<decimal> GetDiscountAmountByAmount(ShoppingCart shoppingCart, Promotion promotion)
        {
            var items = shoppingCart.ShoppingCartItems.Where(x => x.CategoryId == promotion.CategoryId);

            if (items.Sum(x => x.Amount) >= promotion.SpendAmount)
            {
                return promotion.DiscountAmount;
            }

            return 0;
        }
    }
}
