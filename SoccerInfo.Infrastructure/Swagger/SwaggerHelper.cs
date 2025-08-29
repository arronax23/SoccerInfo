using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoccerInfo.Infrastructure.Swagger;
public static class SwaggerHelper
{
    public static void AddUISwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("ui-v1", new OpenApiInfo
            {
                Title = "SoccerInfo API (React)",
                Version = "v1",
                Description = "API for React UI"
            });

            opt.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (apiDesc!.RelativePath!.StartsWith("api/"))
                    return true;

                return false;
            });

            SetupApiKey(opt);
        });

    }
    public static void AddRapidSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("rapid-v1", new OpenApiInfo
            {
                Title = "SoccerInfo Rapid API",
                Version = "v1",
                Description = "Rapid API Endpoints"
            });


            opt.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (apiDesc!.RelativePath!.StartsWith("rapidapi/"))
                    return true;

                return false;
            });
        });
    }


    public static void ConfigureSwaggerUI(this WebApplication? app)
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/ui-v1/swagger.json", "UI API v1");
            c.SwaggerEndpoint("/swagger/rapid-v1/swagger.json", "Rapid API v1");
        });
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
