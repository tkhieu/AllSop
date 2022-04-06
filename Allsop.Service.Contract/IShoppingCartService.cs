using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> AddProductAsync(Guid productId, byte quantity, Guid userId);
        Task<ShoppingCart> RemoveProductAsync(Guid productId, byte quantity, Guid userId);
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(Guid userId);
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync();
        Task<ShoppingCart> ApplyVoucherAsync(string voucher, Guid userId);
    }
}
