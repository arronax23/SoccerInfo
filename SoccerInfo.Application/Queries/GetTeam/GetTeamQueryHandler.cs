using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeam;

internal class GetTeamQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamQuery, TeamDto>
{
    public async Task<TeamDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQuerySingleAsync<TeamDto>(@$"
                SELECT t.Id, t.Name, i.Base64 as LogoBase64, i.MimeType as LogoMimeType FROM Teams t
                JOIN Images i on i.Id = t.LogoId
                WHERE t.Id = @TeamId",
            new { TeamId = request.TeamId });
    }
}
