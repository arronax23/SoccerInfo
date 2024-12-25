using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.FrontendScraper.Parsers;

namespace SoccerInfo.FrontendScraper;
public static class ServiceCollectionExtensions
{
    public static void AddFrontendScraperServices(this IServiceCollection services)
    {
        services.AddScoped<SoccerDataExtractor>();
        services.AddScoped<LeagueParser>();
        services.AddScoped<TeamParser>();
        services.AddScoped<PlayerParser>();
        services.AddScoped<NationalityParser>();
    }
}
