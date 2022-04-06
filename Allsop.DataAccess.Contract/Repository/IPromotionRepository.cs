using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Contract.Repository
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
    }
}
