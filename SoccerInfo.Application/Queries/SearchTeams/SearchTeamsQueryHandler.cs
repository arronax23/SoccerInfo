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
               $@"
                SELECT 
                    t.Id, 
                    t.Name,
                    l.Id as LeagueId,
                    il.Base64 as LeagueLogo,
                    il.MimeType as LeagueLogoMimeType,
                    it.Base64 as TeamLogo,
                    it.MimeType as TeamLogoMimeType       
                FROM Teams t
                JOIN Leagues l on l.Id = t.LeagueId
                LEFT JOIN Images il ON il.Id = l.LogoId
                LEFT JOIN Images it ON it.Id = t.LogoId        
                WHERE t.Name COLLATE Latin1_general_CI_AI
                LIKE @KeywordPhrase COLLATE Latin1_general_CI_AI", new { KeywordPhrase = $"%{request.Keyword}%" }))
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize);
    }
}
