using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Repository.Mapper;

namespace Allsop.Api
{
    public static class MapperServiceStartupExtensions
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<MapperFactory>()
                .AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IMapperFactory, MapperFactory>();
        }
    }
}
