using WP.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddIdentityConfig(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.ResolveSwaggerConfig();

var app = builder.Build();

app.MapDefaultEndpoints();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
