using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface IShoppingCartItemService
    {
        Task<ShoppingCartItem> GetShoppingCartItemAsync(Guid id);
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(IEnumerable<Guid> ids);
        Task<ShoppingCartItem> CreateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task<ShoppingCartItem> CreateOrUpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
    }
}
