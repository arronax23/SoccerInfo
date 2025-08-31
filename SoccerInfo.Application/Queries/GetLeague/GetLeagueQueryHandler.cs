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
                    @$"SELECT l.Id, l.Name,i.Base64 as LogoBase64, i.MimeType as LogoMimeType, l.CountryFlagBase64 FROM Leagues l
                       JOIN Images i on l.LogoId = i.Id
                       WHERE l.Id = @LeagueId",
                    new { LeagueId = request.LeagueId });
    }
}
