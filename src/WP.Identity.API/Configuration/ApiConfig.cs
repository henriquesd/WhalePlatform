namespace WP.Identity.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            // Ensure authentication and authorization are configured before handling requests to controllers
            app.UseIdentityConfiguration();

            app.MapControllers();

            return app;
        }
    }
}
