using Blazored.Toast;
using WP.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddIdentityConfiguration();

builder.Services.AddWebAppConfiguration();

builder.Services.AddBlazoredToast();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseWebAppConfiguration(app.Environment);

app.Run();
