using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeam;

internal class GetTeamQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamQuery, TeamDto>
{
    public async Task<TeamDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQuerySingleAsync<TeamDto>(@$"SELECT Id, Name, TeamImageBase64 FROM Teams where Id = @TeamId",
            new { TeamId = request.TeamId });
    }
}
