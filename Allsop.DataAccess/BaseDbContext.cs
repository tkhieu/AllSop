using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<AllsopDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("allsopDb");

            base.OnConfiguring(optionsBuilder);
        }
    }
}