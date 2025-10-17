using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
using SoccerInfo.Application.Services;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.DbManagement;
using SoccerInfo.Persistence.Repositories;
using SoccerInfo.Persistence.Repositories.Generic;
using System.Text.Json;
using Testcontainers.MsSql;

namespace SoccerInfo.IntegrationTests;

public class Fixture : IAsyncLifetime
{
    private MsSqlContainer _msSqlContainer;
    public ServiceProvider Provider { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
    public IMapper Mapper { get; set; }
    public IPlayerRepository PlayerRepository { get; set; }
    public IGenericRepository<League> LeagueRepository { get; set; }
    public IGenericRepository<Team> TeamRepository { get; set; }
    public IGenericRepository<Nationality> NationalityRepository { get; set; }
    public IGeneralPositionService GeneralPositionService { get; set; }

    public Fixture()
    {
        _msSqlContainer = new MsSqlBuilder().Build();
    }
    
    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        var services = new ServiceCollection();

        services.AddAutoMapper(typeof(Application.IAssemblyMarker));

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(_msSqlContainer.GetConnectionString()));



        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        services.AddLogging();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IGenericRepository<League>, GenericRepository<League>>();
        services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
        services.AddScoped<IGenericRepository<Nationality>, GenericRepository<Nationality>>();
        services.AddScoped<IGeneralPositionService, GeneralPositionService>();
        services.AddScoped<IGeneralPositionLookupRepository, GeneralPositionLookupRepository>();

        Provider = services.BuildServiceProvider();


        var db = Provider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();

        await db.Database.ExecuteSqlRawAsync("INSERT INTO GeneralPositions_Lookup(Name, DisplayName) VALUES ('Goalkeeper','Goalkeepers')");
        await db.Database.ExecuteSqlRawAsync("INSERT INTO GeneralPositions_Lookup(Name, DisplayName) VALUES ('Defender','Defenders')");
        await db.Database.ExecuteSqlRawAsync("INSERT INTO GeneralPositions_Lookup(Name, DisplayName) VALUES ('Midfielder','Midfielders')");
        await db.Database.ExecuteSqlRawAsync("INSERT INTO GeneralPositions_Lookup(Name, DisplayName) VALUES ('Forward','Forwardss')");

        Mapper = Provider.GetRequiredService<IMapper>();
        UnitOfWork = Provider.GetRequiredService<IUnitOfWork>();
        PlayerRepository = Provider.GetRequiredService<IPlayerRepository>();
        LeagueRepository = Provider.GetRequiredService<IGenericRepository<League>>();
        TeamRepository = Provider.GetRequiredService<IGenericRepository<Team>>();
        NationalityRepository = Provider.GetRequiredService<IGenericRepository<Nationality>>();
        GeneralPositionService = Provider.GetRequiredService<IGeneralPositionService>();
    }

    public async Task DisposeAsync()
    {
        Provider.Dispose();
        await _msSqlContainer.DisposeAsync();
    }
}

public class IntergationTests(Fixture fixture) : IClassFixture<Fixture>
{
    [Fact]
    public async Task Test1()
    {
        var handler = new SavePlayersGeneralInfoCommandHandler(
            mapper: fixture.Mapper,
            unitOfWork: fixture.UnitOfWork,
            playerRepository: fixture.PlayerRepository,
            leagueRepository: fixture.LeagueRepository,
            teamRepository: fixture.TeamRepository,
            nationalityRepository: fixture.NationalityRepository,
            generalPositionService: fixture.GeneralPositionService
        );

        string jsonString = File.ReadAllText(Path.Combine("JsonData", "players_general_info_data_36.json"));

        GeneralInfoExtractionData extraction = JsonSerializer.Deserialize<GeneralInfoExtractionData>(jsonString)!;
        var command = new SavePlayersGeneralInfoCommand() { Extraction = extraction };

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal(564, fixture.PlayerRepository.ToQuery().Count());

    }
}