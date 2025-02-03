using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Persistence.JsonData;

namespace SoccerInfo.Persistence;
public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<JsonDataManager>();
    }
}
