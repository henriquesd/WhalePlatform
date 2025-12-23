using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WP.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient for Identity API using configuration
builder.Services.AddScoped<IIdentityService>(sp =>
{
    var identityApiUrl = builder.Configuration["IdentityApi:Url"] ?? "https://localhost:7070";
    var httpClient = new HttpClient { BaseAddress = new Uri(identityApiUrl) };
    return new IdentityService(httpClient);
});

// Configure HttpClient for Catalog API using configuration
builder.Services.AddScoped<ICatalogService>(sp =>
{
    var catalogApiUrl = builder.Configuration["CatalogApi:Url"] ?? "https://localhost:7167";
    var httpClient = new HttpClient { BaseAddress = new Uri(catalogApiUrl) };
    return new CatalogService(httpClient);
});

await builder.Build().RunAsync();
