using Allsop.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class PriceCalculationDbContext: BaseDbContext<PriceCalculationDbContext>
    {
        public DbSet<PromotionDb> Promotions { get; set; }

        public PriceCalculationDbContext(DbContextOptions<PriceCalculationDbContext> options) : base(options)
        {
            GenerateTestData.GeneratePromotionData(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("PriceCalculation");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
