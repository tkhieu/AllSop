using Allsop.DataAccess.Contract.Repository;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;

namespace Allsop.Domain.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        public ShoppingCartItemService(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public Task<ShoppingCartItem> GetShoppingCartItemAsync(Guid id)
            => _shoppingCartItemRepository.GetShoppingCartItemAsync(id);

        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(IEnumerable<Guid> ids)
            => _shoppingCartItemRepository.GetShoppingCartItemsAsync(ids);

        public Task<ShoppingCartItem> CreateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => _shoppingCartItemRepository.CreateShoppingCartItemAsync(shoppingCartItem);

        public Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => _shoppingCartItemRepository.UpdateShoppingCartItemAsync(shoppingCartItem);

        public Task<ShoppingCartItem> CreateOrUpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => _shoppingCartItemRepository.CreateOrUpdateShoppingCartItemAsync(shoppingCartItem);
    }
}
