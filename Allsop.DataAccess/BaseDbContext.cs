using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class BaseDbContext<TContext> : DbContext where TContext: DbContext
    {
        public BaseDbContext(DbContextOptions<TContext> options) : base(options)
        {

        }
    }
}