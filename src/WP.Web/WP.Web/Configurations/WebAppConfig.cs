using WP.Web.Components;

namespace WP.Web.Configurations
{
    public static class WebAppConfig
    {
        public static void AddWebAppConfiguration(this IServiceCollection services)
        {
            services
                .AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
        }

        public static void UseWebAppConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            app.MapDefaultEndpoints();

            app.UseIdentityConfiguration();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                        .AddInteractiveServerRenderMode()
                        .AddInteractiveWebAssemblyRenderMode()
                        .AddAdditionalAssemblies(typeof(WP.Web.Client._Imports).Assembly);
        }
    }
}
