using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;
using Allsop.Service.Contract.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class ProductRepository : BaseRepository<ProductCatalogDbContext>, IProductRepository
    {
        public ProductRepository(ProductCatalogDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var entities = await DbContext.Products
                .Include(x => x.Category).ToListAsync();

            var mapper = MapperFactory.GetMapper<Product, ProductDb>();

            return entities.Select(mapper.From);

        }

        public async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<Guid> ids)
        {
            var entities = await DbContext.Products
                .Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToListAsync();

            var mapper = MapperFactory.GetMapper<Product, ProductDb>();

            return entities.Select(mapper.From);
        }

        public Task<Product> GetProductAsync(Guid id)
            => GetEntityAsync<Product, ProductDb>(id);

        public Task<Product> CreateProductAsync(Product product)
            => CreateEntityAsync<Product, ProductDb>(product);

        public Task<IEnumerable<Product>> CreateProductsAsync(IEnumerable<Product> products)
            => CreateEntitiesAsync<Product, ProductDb>(products);

        public Task<Product> UpdateProductAsync(Product product)
            => UpdateEntityAsync<Product, ProductDb>(product);

        public Task<DeleteResult> DeleteProductAsync(Guid id)
            => DeleteEntityAsync<ProductDb>(id);

        public async Task<Product> IncreaseQuantity(Guid id, int quantity)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new EntityNotFoundException<ProductDb>();
            }

            product.Quantity += quantity;
            await DbContext.SaveChangesAsync();

            var mapper = MapperFactory.GetMapper<Product, ProductDb>();

            return mapper.From(product);
        }

        public async Task<Product> DecreaseQuantity(Guid id, int quantity)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new EntityNotFoundException<ProductDb>();
            }

            if (product.Quantity == 0)
            {
                throw new OutOfStockException(product.Name);
            }

            product.Quantity -= quantity;
            await DbContext.SaveChangesAsync();

            var mapper = MapperFactory.GetMapper<Product, ProductDb>();

            return mapper.From(product);
        }
    }
}