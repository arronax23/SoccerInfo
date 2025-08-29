using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Rapid.API;
using SoccerInfo.Rapid.Application.Utilities;

namespace SoccerInfo.Rapid.Application;

public static class ServiceCollectionExtensions
{
    public static void AddRapidApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IImageUrlGenerator, ImageUrlGenerator>();
    }
}
