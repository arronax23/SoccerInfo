using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayerDetailsDto;

namespace SoccerInfo.Application.Queries.GetPlayerDetails;

internal class GetPlayerDetailsQueryHandler(
    ApplicationDbContext dbContext,
    IMapper mapper) : IQueryHandler<GetPlayerDetailsQuery, PlayerDetailsDto>
{
    public Task<PlayerDetailsDto> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
    {
        var player = dbContext.Players
            .Include(x => x.Nationalities)
            .ThenInclude(y => y.CountryFlag)
            .Include(x => x.MarketValueProgress)
            .SingleOrDefault(x => x.Id == request.PlayerId);

        if (player == null)
            return Task.FromResult(new PlayerDetailsDto());

        var marketValueChanges = player.MarketValueProgress;
        var teams = dbContext.Teams;

        var playerDetailsDto = mapper.Map<PlayerDetailsDto>(player);
        playerDetailsDto.MarketValueChanges = marketValueChanges?.Select(Map);

        return Task.FromResult(playerDetailsDto);
    }

    private MarketValueChangeDto Map(MarketValueChange marketValueChange)
    {
        var team = dbContext.Teams.SingleOrDefault(y => y.Name == marketValueChange.Team);
        string? image = null;

        if (team != null)
            image = team.TeamImageBase64;

        var dto = new MarketValueChangeDto()
        {
            Id = marketValueChange.Id,
            Age = marketValueChange.Age,
            ChangeDate = marketValueChange.ChangeDate,
            MarketValue = marketValueChange.MarketValue,
            MarketValueUnit = marketValueChange.MarketValueUnit,
            PlayerId = marketValueChange.PlayerId,
            TeamImageBase64 = image
        };

        return dto;
    }
}
