using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetStatsFilteringOptions;
internal class GetStatsFilteringOptionsQueryHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetStatsFilteringOptionsQuery, StatsFilteringOptionsDto>
{
    public Task<StatsFilteringOptionsDto> Handle(GetStatsFilteringOptionsQuery request, CancellationToken cancellationToken)
    {
        var dto = new StatsFilteringOptionsDto()
        {
            Positions = dbContext.Players.AsNoTracking().Select(p => p.Position).Distinct().OrderBy(p => p),   
            Teams = dbContext.Teams.AsNoTracking().Distinct().Select(t => t.Name).OrderBy(t => t),
            Leagues = dbContext.Leagues.AsNoTracking().Distinct().Select(t => t.Name).OrderBy(l => l),
            Nationalities = dbContext.Nationalities.AsNoTracking().Distinct().Select(t => t.Country_Lookup).OrderBy(n => n)
        };

        return Task.FromResult(dto);
    }
}
