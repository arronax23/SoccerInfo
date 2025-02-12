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
        var dbPlayers = dbContext.Players.AsNoTracking();
        
        var query = dbPlayers.ApplyFilter(request.Filter);
        var dto = mapper.Map(query, request.Filter.Criteria).AddIndex(request.Filter.PageNumber, request.Filter.PageSize);

        return Task.FromResult(dto);  
    }
}
