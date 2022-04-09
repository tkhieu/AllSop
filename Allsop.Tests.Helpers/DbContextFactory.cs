using Allsop.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Allsop.Tests.Helpers
{
    public static class DbContextFactory
    {
        public static TContext CreateDbContext<TContext>(string dbName) where TContext : BaseDbContext<TContext>
        {
            var options = BuildInMemoryDbOptions<TContext>(dbName);
            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext), options);

            return dbContext;
        }

        private static DbContextOptions<TContext> BuildInMemoryDbOptions<TContext>(string dbName) where TContext: BaseDbContext<TContext>
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return options;
        }
    }
}