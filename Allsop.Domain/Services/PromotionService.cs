using Allsop.Common.Cache;
using Allsop.Common.Exception;
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

        public async Task<decimal> GetDiscountAmountByVoucher(string voucher, decimal spendAmount, Promotion promotion)
        {
            if (!string.Equals(voucher, promotion.Voucher, StringComparison.InvariantCultureIgnoreCase))
            {
                return 0;
            }

            if (spendAmount >= promotion.SpendAmount)
            {
                return promotion.DiscountAmount;
            }

            return 0m;
        }

        public async Task<decimal> GetDiscountAmountByPercent(Guid categoryId, int spendQuantity, Promotion promotion)
        {
            if (categoryId == promotion.CategoryId && spendQuantity >= promotion.SpendQuantity)
            {
                return promotion.DiscountPercent;
            }

            return 0m;
        }

        public async Task<decimal> GetDiscountAmountByAmount(Guid categoryId, decimal spendAmount, Promotion promotion)
        {
            if (categoryId == promotion.CategoryId && spendAmount >= promotion.SpendAmount)
            {
                return promotion.DiscountAmount;
            }

            return 0m;
        }

        public Task<Promotion> GetPromotionByVoucherAsync(string voucher)
        {
            return _promotionRepository.GetPromotionByVoucherAsync(voucher);
        }

        public async Task<bool> CheckValidVoucherAsync(string voucher)
        {
            var promotions = await GetAllPromotionsAsync();
            var promotion = promotions.FirstOrDefault(x =>
                string.Equals(x.Voucher, voucher, StringComparison.InvariantCultureIgnoreCase));

            if (promotion == null)
            {
                throw new InvalidVoucherException(voucher);
            }

            return true;
        }
    }
}
