using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeague;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeagueQuery, LeagueDto>
{
    public async Task<LeagueDto> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor
                .SqlQuerySingleAsync<LeagueDto>(
                    @$"
                    SELECT 
                        l.Id,
                        l.Name,
                        i1.Base64 as LogoBase64,
                        i1.MimeType as LogoMimeType,
                        i2.Base64 as CountryFlagBase64,
                        i2.MimeType as CountryFlagMimeType
                    FROM Leagues l
                    LEFT JOIN Images i1 on l.LogoId = i1.Id
                    LEFT JOIN Images i2 on l.CountryFlagId = i2.Id
                    WHERE l.Id = @LeagueId", 
                    new { LeagueId = request.LeagueId });
    }
}
