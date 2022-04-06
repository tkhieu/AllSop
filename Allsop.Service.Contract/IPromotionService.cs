using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<decimal> GetTotalDiscountAmount(ShoppingCart shoppingCart);
        Task<decimal> GetDiscountAmountByVoucher(ShoppingCart shoppingCart, Promotion promotion);
        Task<decimal> GetDiscountAmountByPercent(ShoppingCart shoppingCart, Promotion promotion);
        Task<decimal> GetDiscountAmountByAmount(ShoppingCart shoppingCart, Promotion promotion);
    }

}
