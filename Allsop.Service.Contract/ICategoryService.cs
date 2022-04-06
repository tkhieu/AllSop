using Allsop.Service.Contract.Model;

namespace Allsop.Service.Contract
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
