using Allsop.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Allsop.Tests.Helpers
{
    public static class DbContextFactory
    {
        public static AllsopDbContext CreateDbContext(string dbName)
        {
            var options = BuildInMemoryDbOptions<AllsopDbContext>(dbName);

            return new AllsopDbContext(options);
        }

        private static DbContextOptions<T> BuildInMemoryDbOptions<T>(string dbName) where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return options;
        }
    }
}