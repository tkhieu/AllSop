using Allsop.Common.Cache;
using Allsop.DataAccess.Contract.Repository;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Allsop.Domain
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private const string _cachedKey = "allProducts";

        public ProductService(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            if (_cache.TryGetValue(_cachedKey, out CacheItemModel<IEnumerable<Product>> cacheItemsModel))
            {
                return cacheItemsModel.Data;
            }

            var products = await _productRepository.GetAllProductsAsync();

            _cache.Set(_cachedKey, new CacheItemModel<IEnumerable<Product>>(products));

            return products;
        }

        public Task<Product> GetProductAsync(Guid id)
            => _productRepository.GetProductAsync(id);

        public Task<Product> IncreaseQuantity(Guid id, int quantity)
        {
            return _productRepository.IncreaseQuantity(id, quantity);
        }

        public Task<Product> DecreaseQuantity(Guid id, int quantity)
        {
            return _productRepository.DecreaseQuantity(id, quantity);
        }
    }
}