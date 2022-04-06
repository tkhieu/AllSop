using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Contract.Repository
{
    public interface IShoppingCartItemRepository
    {
        Task<ShoppingCartItem> GetShoppingCartItemAsync(Guid id);
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(IEnumerable<Guid> ids);
        Task<ShoppingCartItem> CreateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task<ShoppingCartItem> CreateOrUpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
    }
}
