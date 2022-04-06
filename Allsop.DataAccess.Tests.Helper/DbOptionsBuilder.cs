using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Allsop.DataAccess.Tests.Helper
{
    public static class DbOptionsBuilder
    {
        public static DbContextOptions<T> BuildInMemoryDbOptions<T>(string dbName, ILoggerFactory factory = null) where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: dbName)
                .UseLoggerFactory(factory ?? MyLoggerFactory)
                .Options;
            return options;
        }
    }
}