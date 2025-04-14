﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace WP.AppHost.Extensions
{
    internal static class ResourceBuilderExtensions
    {
        internal static IResourceBuilder<T> WithSwaggerUI<T>(this IResourceBuilder<T> builder)
            where T: IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs("swagger-ui-docs", "Swagger API Documentation", "swagger");
        }

        internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
            where T : IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs("scalar-docs", "Scalar API Documentation", "scalar/v1");
        }

        internal static IResourceBuilder<T> withReDoc<T>(this IResourceBuilder<T> builder)
            where T : IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs("redoc-docs", "ReDoc API Documentation", "api-docs");
        }

        private static IResourceBuilder<T> WithOpenApiDocs<T>(
            this IResourceBuilder<T> builder,
            string name,
            string displayName,
            string openApiUiPath)
            where T : IResourceWithEndpoints
        {
            return builder.WithCommand(
                name,
                displayName,
                executeCommand: async _ =>
                {
                    try
                    {
                        // Base URL
                        var endpoint = builder.GetEndpoint("https");

                        var url = $"{endpoint.Url}/{openApiUiPath}";

                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                        return new ExecuteCommandResult { Success = true };
                    }
                    catch (Exception ex)
                    {
                        return new ExecuteCommandResult { Success = false, ErrorMessage = ex.ToString() };
                    }
                },
                updateState: context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy
                   ? ResourceCommandState.Enabled
                   : ResourceCommandState.Disabled,
                iconName: "Document",
                iconVariant: IconVariant.Filled);
        }
    }
}
