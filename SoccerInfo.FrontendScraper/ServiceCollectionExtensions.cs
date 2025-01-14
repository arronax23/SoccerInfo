using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.FrontendScraper.AcceptCookies;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
using SoccerInfo.FrontendScraper.Utilities;

namespace SoccerInfo.FrontendScraper;
public static class ServiceCollectionExtensions
{
    public static void AddFrontendScraperServices(this IServiceCollection services)
    {
        services.AddSingleton<PuppeteerManager>();
        services.AddSingleton<PlaywrightManager>();

        services.AddScoped<PlayersGeneralInfoExtractor>();
        services.AddScoped<LeagueParser>();
        services.AddScoped<TeamParser>();
        services.AddScoped<PlayerParser>();
        services.AddScoped<NationalityParser>();

        services.AddScoped<PlayersCharacteristicsExtractor>();
        services.AddScoped<CookieReader>();
        services.AddScoped<InfoTableParser>();
        services.AddScoped<NationalTeamParser>();
        services.AddScoped<SocialsParser>();
        services.AddScoped<StatsParser>();

        services.AddScoped<CookiesExtractor>();

    }
}
