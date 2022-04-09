using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class ShoppingCartItemRepository : BaseRepository<ShoppingCartDbContext>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(ShoppingCartDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public Task<ShoppingCartItem> GetShoppingCartItemAsync(Guid id)
            => GetEntityAsync<ShoppingCartItem, ShoppingCartItemDb>(id);

        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(IEnumerable<Guid> ids) 
            => GetEntitiesAsync<ShoppingCartItem, ShoppingCartItemDb>(ids);

        public Task<ShoppingCartItem> CreateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => CreateEntityAsync<ShoppingCartItem, ShoppingCartItemDb>(shoppingCartItem);

        public Task<ShoppingCartItem> UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => UpdateEntityAsync<ShoppingCartItem, ShoppingCartItemDb>(shoppingCartItem);

        public Task<ShoppingCartItem> CreateOrUpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
            => CreateOrUpdateEntityAsync<ShoppingCartItem, ShoppingCartItemDb>(shoppingCartItem);
    }
}
