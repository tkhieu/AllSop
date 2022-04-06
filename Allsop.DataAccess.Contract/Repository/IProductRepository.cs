using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Contract.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<Guid> ids);
        Task<Product> GetProductAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> IncreaseQuantity(Guid id, int quantity);
        Task<Product> DecreaseQuantity(Guid id, int quantity);
    }
}
