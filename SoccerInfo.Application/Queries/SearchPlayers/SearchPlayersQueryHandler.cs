using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;


namespace SoccerInfo.Application.Queries.SearchPlayers;

internal class SearchPlayersQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<SearchPlayersQuery, IEnumerable<PlayerOverviewDto>>
{
    public Task<IEnumerable<PlayerOverviewDto>> Handle(SearchPlayersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQueryRaw<PlayerOverviewDto>(
                $@"SELECT 
                    p.Id, 
                    p.Name,
                    p.FaceImageBase64,
                    t.Id as TeamId, 
                    t.TeamImageBase64,
                    l.Id as LeagueId,
                    l.LeagueImageBase64 FROM Players p
                  JOIN Teams t ON  t.Id = p.TeamId 
                  JOIN Leagues l on l.Id = t.LeagueId
                  WHERE p.Name like '%{request.Keyword}%'")
            .AsEnumerable());
    }
}