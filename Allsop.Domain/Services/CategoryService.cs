using Allsop.Common.Cache;
using Allsop.DataAccess.Contract.Repository;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Allsop.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;
        private const string _cachedKey = "allCategories";

        public CategoryService(ICategoryRepository categoryRepository, IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            if (_cache.TryGetValue(_cachedKey, out CacheItemModel<IEnumerable<Category>> cacheItemsModel))
            {
                return cacheItemsModel.Data;
            }

            var categories = await _categoryRepository.GetAllCategoriesAsync();

            _cache.Set(_cachedKey, new CacheItemModel<IEnumerable<Category>>(categories));

            return categories;
        }
    }
}
