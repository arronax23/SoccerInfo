using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.SearchTeams;
internal class SearchTeamsQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<SearchTeamsQuery, IEnumerable<TeamOverviewDto>>
{
    public async Task<IEnumerable<TeamOverviewDto>> Handle(SearchTeamsQuery request, CancellationToken cancellationToken)
    {
        return (await
           sqlExecutor
           .SqlQueryAsync<TeamOverviewDto>(
               $@"SELECT 
                    t.Id, 
                    t.Name,
                    t.TeamImageBase64,
                    l.Id as LeagueId,
                    l.LeagueImageBase64 FROM Teams t
                 JOIN Leagues l on l.Id = t.LeagueId
                 WHERE t.Name COLLATE Latin1_general_CI_AI
                 LIKE @KeywordPhrase COLLATE Latin1_general_CI_AI", new { KeywordPhrase = $"%{request.Keyword}%" }))
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize);
    }
}
