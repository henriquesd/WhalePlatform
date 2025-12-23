using WP.Web.Client.Pages;
using WP.Web.Components;
using WP.Web.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Register HTTP services for Server-side rendering
builder.Services.AddScoped<IIdentityService>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7070") };
    return new IdentityService(httpClient);
});

builder.Services.AddScoped<ICatalogService>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7167") };
    return new CatalogService(httpClient);
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WP.Web.Client._Imports).Assembly);

app.Run();
