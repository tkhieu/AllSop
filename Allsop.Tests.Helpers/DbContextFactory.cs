using Allsop.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Allsop.Tests.Helpers
{
    public static class DbContextFactory
    {
        public static TContext CreateDbContext<TContext>(string dbName) where TContext : DbContext
        {
            var options = BuildInMemoryDbOptions<TContext>(dbName);
            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext), options);

            return dbContext;
        }

        private static DbContextOptions<BaseDbContext<TContext>> BuildInMemoryDbOptions<TContext>(string dbName) where TContext: DbContext
        {
            var options = new DbContextOptionsBuilder<BaseDbContext<TContext>>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return options;
        }
    }
}