using Microsoft.OpenApi.Models;
using SoccerInfo.API.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoccerInfo.Infrastructure.Swagger;

public class DefaultApiKeyOperationFilter(IConfiguration configuration) : IOperationFilter
{
    private readonly string apiKey = configuration.GetValue<string>(AuthConstants.ApiKeyAppsettingsSection)!;

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {

        var allowedMethods = new[] { "PUT", "POST", "PATCH" };
        if (allowedMethods.Contains(context.ApiDescription.HttpMethod?.ToUpperInvariant()))
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
                Required = false,
                Description = "API Key for authentication",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new Microsoft.OpenApi.Any.OpenApiString(apiKey)
                }
            });
        }
    }
}