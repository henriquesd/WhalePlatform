using WP.Web.Interfaces;
using WP.Web.Services;

namespace WP.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }
    }
}
