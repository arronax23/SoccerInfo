using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SoccerInfo.Infrastructure.Swagger;
internal static class SwaggerHelper
{
    public static void Configure(SwaggerGenOptions options)
    {
        options.CustomSchemaIds(x => x.FullName);

        options.SwaggerDoc("ui-v1", new OpenApiInfo
        {
            Title = "SoccerInfo API (React)",
            Version = "v1",
            Description = "API for React UI"
        });
        options.SwaggerDoc("rapid-v1", new OpenApiInfo
        {
            Title = "SoccerInfo Rapid API",
            Version = "v1",
            Description = "Rapid API Endpoints"
        });



        options.DocInclusionPredicate((docName, apiDesc) =>
        {
            if (docName == "ui-v1" && apiDesc.RelativePath!.StartsWith("api/", StringComparison.OrdinalIgnoreCase))
                return true;

            if (docName == "rapid-v1" && apiDesc.RelativePath!.StartsWith("rapidapi/", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        });

        SetupApiKey(options);
    }

    public static void ConfigureUI(SwaggerUIOptions options)
    {
        options.SwaggerEndpoint("/swagger/ui-v1/swagger.json", "UI API v1");
        options.SwaggerEndpoint("/swagger/rapid-v1/swagger.json", "Rapid API v1");
    }

    private static void SetupApiKey(SwaggerGenOptions options)
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
