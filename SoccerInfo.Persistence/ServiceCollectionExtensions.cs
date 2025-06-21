using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Intrefaces;
using SoccerInfo.Application.JsonFileData;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Domain.Models.Stats;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Persistence.DbManagement;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Persistence.Repositories;
using SoccerInfo.Persistence.Repositories.Generic;
using SoccerInfo.Persistence.Sql;

namespace SoccerInfo.Persistence;
public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<JsonFileDataManager>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IGeneralPositionLookupRepository, GeneralPositionLookupRepository>();

        services.AddScoped<IJsonFileDataManager, JsonFileDataManager>();

        services.AddTransient<ISqlExecutor, SqlExecutor>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenericRepository<League>, GenericRepository<League>>();
        services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
        services.AddScoped<IGenericRepository<Nationality>, GenericRepository<Nationality>>();
        services.AddScoped<IGenericRepository<PlayerCharacteristic>, GenericRepository<PlayerCharacteristic>>();
        services.AddScoped<IGenericRepository<PlayerStatistic>, GenericRepository<PlayerStatistic>>();
        services.AddScoped<IGenericRepository<LeagueLinkLookup>, GenericRepository<LeagueLinkLookup>>();
        services.AddScoped<IGenericRepository<CountryFlag_Lookup>, GenericRepository<CountryFlag_Lookup>>();
        services.AddScoped<IGenericRepository<StatsLeague>, GenericRepository<StatsLeague>>();




    }
}
