using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;

internal class GetPlayersByStatsQueryHandler(QueryMapper mapper, ApplicationDbContext dbContext) 
    : IQueryHandler<GetPlayersByStatsQuery, IEnumerable<StatsPlayerBaseDto>>
{
    public Task<IEnumerable<StatsPlayerBaseDto>> Handle(GetPlayersByStatsQuery request, CancellationToken cancellationToken)
    {
        var dbPlayers = dbContext.Players
            .Include(p => p.Stats)
            .Include(p => p.Team)
            .ThenInclude(t => t.League)
            .AsNoTracking();
        
        var query = dbPlayers.ApplyFilter(request.Filter);

        return Task.FromResult(mapper.Map(query, request.Filter.Criteria));  
    }
}
