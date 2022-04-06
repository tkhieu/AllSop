using Allsop.Common.Mediator;
using Microsoft.Extensions.Caching.Memory;

namespace Allsop.Api
{
    public static class CommonServiceStartupExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddRequestScoped<IScopedMediator, ScopedMediator>();
            services.AddRequestScoped<IMemoryCache, MemoryCache>();
        }
    }
}
