using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.API.Authorization;

namespace SoccerInfo.API;

public static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ApiKeyAuthorizationFilter>();
    }
}