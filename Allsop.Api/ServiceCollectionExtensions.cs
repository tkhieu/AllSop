using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Allsop.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<TImplementation>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services.AddTransient<TService>(provider =>
                provider.GetRequiredService<IHttpContextAccessor>().HttpContext?.RequestServices.GetRequiredService<TImplementation>()
                ?? provider.GetRequiredService<TImplementation>()
            );
        }
    }
}
