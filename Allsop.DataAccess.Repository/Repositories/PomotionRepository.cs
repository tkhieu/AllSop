using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Contract.Repository;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Repositories.Base;
using Allsop.Service.Contract.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess.Repository.Repositories
{
    public class PromotionRepository : BaseRepository<PriceCalculationDbContext>, IPromotionRepository
    {
        public PromotionRepository(PriceCalculationDbContext dbContext, IMapperFactory mapperFactory) : base(dbContext, mapperFactory)
        {
        }

        public Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
            => GetAllEntitiesAsync<Promotion, PromotionDb>();

        public async Task<Promotion> GetPromotionByVoucherAsync(string voucher)
        {
            var entitity = await DbContext.Promotions.FirstOrDefaultAsync(x =>
                string.Equals(x.Voucher, voucher, StringComparison.InvariantCultureIgnoreCase));

            if (entitity == null)
            {
                throw new InvalidVoucherException(voucher);
            }

            var mapper = MapperFactory.GetMapper<Promotion, PromotionDb>();

            return mapper.From(entitity);
        }
    }
}
