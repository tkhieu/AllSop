using Allsop.Domain;
using Allsop.Domain.Services;
using Allsop.Service.Contract;

namespace Allsop.Api
{
    public static class DomainServiceStartupExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }
    }
}
