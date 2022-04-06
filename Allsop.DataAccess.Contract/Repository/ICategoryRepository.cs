using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Contract.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
