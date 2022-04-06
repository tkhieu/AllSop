using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(Guid id);
        Task<Product> IncreaseQuantity(Guid id, int quantity);
        Task<Product> DecreaseQuantity(Guid id, int quantity);
    }
}