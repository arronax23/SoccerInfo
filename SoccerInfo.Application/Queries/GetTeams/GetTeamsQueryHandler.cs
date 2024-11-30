using SoccerInfo.Infrastructure.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.GetTeams;

internal class GetTeamsQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetTeamsQuery, IEnumerable<TeamDto>>
{
    public Task<IEnumerable<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQuery<TeamDto>(@"SELECT [Id], [Name] FROM Teams")
            .AsEnumerable());
    }
}
