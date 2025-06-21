using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.SearchPlayers;

internal class SearchPlayersQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<SearchPlayersQuery, IEnumerable<PlayerOverviewDto>>
{
    public async Task<IEnumerable<PlayerOverviewDto>> Handle(SearchPlayersQuery request, CancellationToken cancellationToken)
    {
        return (await 
            sqlExecutor
            .SqlQueryAsync<PlayerOverviewDto>(
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
                  WHERE p.Name COLLATE Latin1_general_CI_AI
                  LIKE @KeywordPhrase COLLATE Latin1_general_CI_AI", new { KeywordPhrase = $"%{request.Keyword}%" }))
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
    }
}