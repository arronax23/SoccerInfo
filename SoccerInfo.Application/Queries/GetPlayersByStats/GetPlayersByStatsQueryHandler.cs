using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;

internal class GetPlayersByStatsQueryHandler(QueryMapper mapper, IPlayerRepository playerRepository) 
    : IQueryHandler<GetPlayersByStatsQuery, IEnumerable<StatsPlayerBaseDto>>
{
    public Task<IEnumerable<StatsPlayerBaseDto>> Handle(GetPlayersByStatsQuery request, CancellationToken cancellationToken)
    {
        var dbPlayers = playerRepository.ToQuery().AsNoTracking();
        
        var query = dbPlayers.ApplyFilter(request.Filter);
        var dto = mapper.Map(query, request.Filter.Criteria).AddIndex(request.Filter.PageNumber, request.Filter.PageSize);

        return Task.FromResult(dto);  
    }
}
