using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class PromotionRepository : BaseRepository, IPromotionRepository
    {
        public PromotionRepository(AllsopDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
            => GetAllEntitiesAsync<Promotion, PromotionDb>();
    }
}
