using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeams;

internal class GetTeamsQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamsQuery, IEnumerable<TeamDto>>
{
    public async Task<IEnumerable<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQueryAsync<TeamDto>(@$"SELECT Id, Name, TeamImageBase64 FROM Teams where LeagueId = @LeagueId",
            new { LeagueId = request.LeagueId });
    }
}
