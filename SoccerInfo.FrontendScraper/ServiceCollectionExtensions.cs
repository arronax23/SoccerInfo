using Microsoft.Extensions.DependencyInjection;
using Polly;
using SoccerInfo.FrontendScraper.AcceptCookies;
using SoccerInfo.FrontendScraper.Resilience;
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
        services.AddScoped<ImageFetcher>();

        services.AddScoped<PlayersGeneralInfoExtractor>();
        services.AddScoped<Traverser>();
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
        services.AddResiliencePipeline(CharacteristicsExtractionPipeline.Name, CharacteristicsExtractionPipeline.Configure);
        services.AddResiliencePipeline(GeneralInfoExtractionPipeline.Name, GeneralInfoExtractionPipeline.Configure);


        services.AddScoped<CookiesExtractor>();

    }
}
