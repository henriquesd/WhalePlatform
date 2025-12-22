using WP.Catalog.API.Data;
using WP.Catalog.API.Repositories;

namespace WP.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}