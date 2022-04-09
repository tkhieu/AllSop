using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion> GetPromotionByVoucherAsync(string voucher);
        Task<bool> CheckValidVoucherAsync(string voucher);
        Task<decimal> GetDiscountAmountByVoucher(string voucher, decimal spendAmount, Promotion promotion);
        Task<decimal> GetDiscountAmountByPercent(Guid categoryId, int spendQuantity, Promotion promotion);
        Task<decimal> GetDiscountAmountByAmount(Guid categoryId, decimal spendAmount, Promotion promotion);
    }

}
