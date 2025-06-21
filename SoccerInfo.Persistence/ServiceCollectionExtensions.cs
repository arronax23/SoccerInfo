using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Persistence.Repositories;

namespace SoccerInfo.Persistence;
public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<JsonFileDataManager>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IGeneralPositionLookupRepository, GeneralPositionLookupRepository>();
        services.AddScoped<IPlayerCharacteristicsRepository, PlayerCharacteristicsRepository>();
        services.AddScoped<GeneralPositionService>();
    }
}
