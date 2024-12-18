using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeam;

internal class GetTeamQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamQuery, TeamDto>
{
    public Task<TeamDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQuery<TeamDto>(@$"SELECT Id, Name, TeamImageBase64 FROM Teams where Id = {request.TeamId}")
            .Single());
    }
}
