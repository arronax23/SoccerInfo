using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoccerInfo.Infrastructure.Swagger;

public static class SwaggerHelper
{
    public static void Setup(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "API Key needed to access the endpoints. Add it to the request header with the name 'X-API-KEY'.",
            Type = SecuritySchemeType.ApiKey,
            Name = "X-Api-Key",
            In = ParameterLocation.Header,
            Scheme = "ApiKey"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    Scheme = "ApiKey",
                    Name = "ApiKey",
                    In = ParameterLocation.Header
                },
                new List<string>(){}
            }
        });

        options.OperationFilter<DefaultApiKeyOperationFilter>();
    }
}
