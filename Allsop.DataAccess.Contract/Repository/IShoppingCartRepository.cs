using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Contract.Repository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(Guid id);
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(Guid userId);
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync();
        Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart> ApplyVoucherByUserIdAsync(string voucher, Guid userId);
    }
}
