using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeams;

internal class GetTeamsQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamsQuery, IEnumerable<TeamDto>>
{
    public async Task<IEnumerable<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQueryAsync<TeamDto>(@$"               
                SELECT t.Id, t.Name,i.Base64 as LogoBase64, i.MimeType as LogoMimeType FROM Teams t 
                JOIN Images i on i.Id = t.LogoId
                WHERE LeagueId = @LeagueId",
            new { LeagueId = request.LeagueId });
    }
}
