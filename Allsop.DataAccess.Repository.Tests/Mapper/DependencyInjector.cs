using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Repository.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    internal static class DependencyInjector
    {
        public static IServiceProvider GetServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.Scan(scan =>
                scan
                    .FromAssemblyOf<MapperFactory>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            services.AddScoped<IMapperFactory, MapperFactory>();

            return services.BuildServiceProvider();
        }
    }
}
