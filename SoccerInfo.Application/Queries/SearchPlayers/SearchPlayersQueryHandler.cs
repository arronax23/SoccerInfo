using SoccerInfo.Application.Abstractions.Interfaces;
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
                $@"
                    SELECT 
                        p.Id, 
                        p.Name,
                        t.Id as TeamId, 
                        l.Id as LeagueId,
                        il.Base64 as LeagueLogo,
                        il.MimeType as LeagueLogoMimeType,
                        it.Base64 as TeamLogo,
                        it.MimeType as TeamLogoMimeType,    
                        ip.Base64 as FaceImage,
                        ip.MimeType as FaceImageMimeType
                    FROM Players p
                    JOIN Teams t ON t.Id = p.TeamId 
                    JOIN Leagues l ON l.Id = t.LeagueId
                    LEFT JOIN Images il ON il.Id = l.LogoId
                    LEFT JOIN Images it ON it.Id = t.LogoId
                    LEFT JOIN Images ip ON ip.Id = p.FaceImageId
                    WHERE p.Name COLLATE Latin1_general_CI_AI
                    LIKE @KeywordPhrase COLLATE Latin1_general_CI_AI", new { KeywordPhrase = $"%{request.Keyword}%" }))
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
    }
}