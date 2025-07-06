using Microsoft.Extensions.DependencyInjection;
using Polly;
using SoccerInfo.Application.Commands.ExtractMarketValueProgress;
using SoccerInfo.Application.Commands.ExtractNationalities;
using SoccerInfo.BackendScraper;

namespace SoccerInfo.Application;
public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CustomMapper>();

        services.AddScoped<Queries.GetPlayersByStats.QueryMapper>();

        services.AddResiliencePipeline(NationalityExtractionPipeline.Name, NationalityExtractionPipeline.Configure);
    }
}
