using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayerDetailsDto;

namespace SoccerInfo.Application.Queries.GetPlayerDetails;

internal class GetPlayerDetailsQueryHandler(
    IPlayerRepository playerRepository,
    IGenericRepository<Team> teamRepository,
    IMapper mapper) : IQueryHandler<GetPlayerDetailsQuery, PlayerDetailsDto>
{
    public Task<PlayerDetailsDto> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
    {
        var player = playerRepository.ToQuery().SingleOrDefault(x => x.Id == request.PlayerId);

        if (player == null)
            return Task.FromResult(new PlayerDetailsDto());

        var playerDetailsDto = mapper.Map<PlayerDetailsDto>(player);

        if (player.MarketValueProgress != null && player.MarketValueProgress.Count > 0)
            playerDetailsDto.MarketValueChanges = player.MarketValueProgress?.Select(Map);

        return Task.FromResult(playerDetailsDto);
    }

    private MarketValueChangeDto Map(MarketValueChange marketValueChange)
    {
        var team = teamRepository.ToQuery().SingleOrDefault(y => y.Name == marketValueChange.Team);
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
