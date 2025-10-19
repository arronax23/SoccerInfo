using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
using SoccerInfo.Application.Services;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using System.Linq.Expressions;
using System.Text.Json;

namespace SoccerInfo.UnitTests;


public class TestServicesFixture
{
    public IServiceProvider Services { get; }

    public TestServicesFixture()
    {
        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(Application.IAssemblyMarker)); 

        Services = services.BuildServiceProvider();
    }
}

public class UnitTest1 : IClassFixture<TestServicesFixture>
{
    private readonly IMapper _mapper;
    public UnitTest1(TestServicesFixture fixture)
    {
        _mapper = fixture.Services.GetRequiredService<IMapper>();
    }

    [Fact]
    public async Task Handle_ShouldAddNewLeague_WhenLeagueDoesNotExist()
    {
        // Arrange
        var leagueRepoMock = new Mock<IGenericRepository<League>>();
        leagueRepoMock
            .Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<League, bool>>>()))
            .ReturnsAsync((League?)null);


        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var handler = new SavePlayersGeneralInfoCommandHandler(
            mapper: _mapper,
            unitOfWorkMock.Object,
            playerRepository: Mock.Of<IPlayerRepository>(),
            leagueRepoMock.Object,
            teamRepository: Mock.Of<IGenericRepository<Team>>(),
            nationalityRepository: Mock.Of<IGenericRepository<Nationality>>(),
            generalPositionService: Mock.Of<IGeneralPositionService>()
        );

        string jsonString = File.ReadAllText(Path.Combine("JsonData", "players_general_info_data_36.json"));

        GeneralInfoExtractionData extraction = JsonSerializer.Deserialize<GeneralInfoExtractionData>(jsonString)!;
        var command = new SavePlayersGeneralInfoCommand(){ Extraction = extraction };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        leagueRepoMock.Verify(r => r.AddAsync(It.IsAny<League>()), Times.AtLeastOnce);
    }

}