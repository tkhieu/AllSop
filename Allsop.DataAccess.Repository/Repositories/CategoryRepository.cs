using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class CategoryRepository : BaseRepository<ProductCatalogDbContext>, ICategoryRepository
    {
        public CategoryRepository(ProductCatalogDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
            => GetAllEntitiesAsync<Category, CategoryDb>();
    }
}
