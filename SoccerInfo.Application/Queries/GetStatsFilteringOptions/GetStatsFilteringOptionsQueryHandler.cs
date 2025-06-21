using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetStatsFilteringOptions;
internal class GetStatsFilteringOptionsQueryHandler(
    IPlayerRepository playerRepository,
    IGenericRepository<League> leagueRepository,
    IGenericRepository<Team> teamRepository,
    IGenericRepository<Nationality> nationalityRepository)
    : IQueryHandler<GetStatsFilteringOptionsQuery, StatsFilteringOptionsDto>
{
    public Task<StatsFilteringOptionsDto> Handle(GetStatsFilteringOptionsQuery request, CancellationToken cancellationToken)
    {
        var dto = new StatsFilteringOptionsDto()
        {
            Positions = playerRepository.ToQuery().AsNoTracking().Select(p => p.Position).Distinct().OrderBy(p => p),   
            Teams = teamRepository.ToQuery().AsNoTracking().Distinct().Select(t => t.Name).OrderBy(t => t),
            Leagues = leagueRepository.ToQuery().AsNoTracking().Distinct().Select(t => t.Name).OrderBy(l => l),
            Nationalities = nationalityRepository.ToQuery().AsNoTracking().Distinct().Select(t => t.Country_Lookup).OrderBy(n => n)!
        };

        return Task.FromResult(dto);
    }
}
