using Microsoft.EntityFrameworkCore;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Queries.GetLeagueTeams;
using SoccerInfo.Rapid.Application.Utilities;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;
internal class GetLeagueTeamsQueryHandler(
    IGenericRepository<League> repository, 
    IImageUrlGenerator imageUrlGenerator) : IQueryHandler<GetLeagueTeamsQuery, LeagueTeamsDto?>
{
    public async Task<LeagueTeamsDto?> Handle(GetLeagueTeamsQuery request, CancellationToken cancellationToken)
    {
        var league = await repository.ToQuery().SingleOrDefaultAsync(l => l.Id == request.LeagueId);

        if (league is not null)
        {
            return new LeagueTeamsDto()
            {
                Id = league.Id,
                Name = league.Name,
                Teams = league.Teams.Select(t => new LeagueTeamsDto.TeamDto()
                {
                    Id = t.Id,
                    LogoUrl = t.LogoId != null ? imageUrlGenerator.Generate(t.LogoId.Value) : null,
                    Name = t.Name,
                    PlayersCount = t.Players.Count
                })
            };
        }
        else
            return null;
    }
}
