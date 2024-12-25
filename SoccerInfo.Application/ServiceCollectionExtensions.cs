using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Application.Commands.ExtractBackend;
using SoccerInfo.Application.Commands.UpdateToEnglish;
using SoccerInfo.BackendScraper;

namespace SoccerInfo.Application;
public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<UpdateNationalitiesToEnglishService>();
        services.AddScoped<MarketValueProgressScraper>();
        services.AddScoped<CustomMapper>();
    }
}
