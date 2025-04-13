using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace WP.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void ResolveSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Whale Platform Identity API",
                    Description = "Identity API for Whale Platform",
                    Contact = new OpenApiContact() { Name = "Henrique", Email = "demo@demo.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            services.ConfigureSwaggerGen(options => options.ExampleFilters());
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        public static void ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
        }
    }
}
