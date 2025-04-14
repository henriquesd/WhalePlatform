using Scalar.AspNetCore;

namespace WP.Identity.API.Configuration
{
    public static class OpenApiConfig
    {
        public static void AddOpenApiConfiguration(this IServiceCollection services)
        {
            services.AddOpenApi();
        }

        public static void UseOpenApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                // Swagger
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "WP Identity API");
                });

                // ReDoc
                app.UseReDoc(options =>
                {
                    options.SpecUrl("/openapi/v1.json");
                });

                // Scalar
                app.MapScalarApiReference();
            }
        }
    }
}
