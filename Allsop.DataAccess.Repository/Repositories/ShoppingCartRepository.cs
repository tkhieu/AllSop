using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class ShoppingCartRepository: BaseRepository<ShoppingCartDbContext>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ShoppingCartDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(Guid id)
        {
            var entity = await DbContext.ShoppingCarts
                .Include(x => x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException<ShoppingCartDb>();
            }

            var mapper = MapperFactory.GetMapper<ShoppingCart, ShoppingCartDb>();

            return mapper.From(entity);
        }

        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(Guid userId)
        {
            var entity = await DbContext.ShoppingCarts
                .Include(x=>x.ShoppingCartItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (entity == null)
            {
                throw new EntityNotFoundException<ShoppingCartDb>();
            }

            var mapper = MapperFactory.GetMapper<ShoppingCart, ShoppingCartDb>();

            return mapper.From(entity);
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync()
        {
            var entities = await DbContext.ShoppingCarts
                .Include(x => x.ShoppingCartItems).ToListAsync();

            if (!entities.Any())
            {
                throw new EntityNotFoundException<ShoppingCartDb>();
            }

            var mapper = MapperFactory.GetMapper<ShoppingCart, ShoppingCartDb>();

            return entities.Select(mapper.From);
        }

        public Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            return CreateEntityAsync<ShoppingCart, ShoppingCartDb>(shoppingCart);
        }

        public Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            return UpdateEntityAsync<ShoppingCart, ShoppingCartDb>(shoppingCart);
        }

        public async Task<ShoppingCart> ApplyVoucherByUserIdAsync(string voucher, Guid userId)
        {
            var entity = await DbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);

            if (entity == null)
            {
                throw new EntityNotFoundException<ShoppingCartDb>();
            }

            entity.Voucher = voucher;
            await DbContext.SaveChangesAsync();

            var mapper = MapperFactory.GetMapper<ShoppingCart, ShoppingCartDb>();

            return mapper.From(entity);
        }
    }
}
