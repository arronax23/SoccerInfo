using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;

namespace SoccerInfo.FrontendScraper;
public static class ServiceCollectionExtensions
{
    public static void AddFrontendScraperServices(this IServiceCollection services)
    {
        services.AddScoped<PlayersGeneralInfoExtractor>();
        services.AddScoped<LeagueParser>();
        services.AddScoped<TeamParser>();
        services.AddScoped<PlayerParser>();
        services.AddScoped<NationalityParser>();
    }
}
