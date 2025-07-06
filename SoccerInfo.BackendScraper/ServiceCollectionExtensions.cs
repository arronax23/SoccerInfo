using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.BackendScraper.MarketValueProgress;
using SoccerInfo.BackendScraper.PlayersTransfers;

namespace SoccerInfo.BackendScraper;
public static class ServiceCollectionExtensions
{
    public static void AddBackendScraperServices(this IServiceCollection services)
    {
        services.AddScoped<MarketValueProgressScraper>();
        services.AddScoped<PlayersTransfersScraper>();
    }
}
