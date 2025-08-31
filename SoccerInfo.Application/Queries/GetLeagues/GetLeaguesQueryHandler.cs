using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeagues;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeaguesQuery, IEnumerable<LeagueDto>>
{
    public async Task<IEnumerable<LeagueDto>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQueryAsync<LeagueDto>(
                @"SELECT l.Id, l.Name,i.Base64 as LogoBase64, i.MimeType as LogoMimeType, l.CountryFlagBase64  FROM Leagues l
                  JOIN Images i on l.LogoId = i.Id");
    }
}
