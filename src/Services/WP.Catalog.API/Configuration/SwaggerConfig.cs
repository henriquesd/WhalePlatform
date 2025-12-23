using Microsoft.AspNetCore.Mvc;

namespace WP.Catalog.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Configura o Swagger para respeitar atributos de serialização
                c.UseInlineDefinitionsForEnums();
                c.CustomSchemaIds(type => type.FullName);
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API v1");
                c.RoutePrefix = "swagger";
            });
        }
    }
}