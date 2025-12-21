using WP.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WP_Web>("webfrontend")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.WP_Identity_API>("wp-identity-api")
    .WithSwaggerUI()
    .WithScalar()
    .WithReDoc();

builder.AddProject<Projects.WP_Cart_API>("wp-cart-api");

builder.AddProject<Projects.WP_Catalog_API>("wp-catalog-api");

builder.AddProject<Projects.WP_Customer_API>("wp-customer-api");

builder.AddProject<Projects.WP_Payment_API>("wp-payment-api");

builder.AddProject<Projects.WP_Order_API>("wp-order-api");

builder.AddProject<Projects.WP_Web>("wp-web");

builder.Build().Run();
