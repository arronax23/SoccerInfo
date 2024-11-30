using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeams;

public class GetTeamsQuery : IQuery<IEnumerable<TeamDto>>
{
}

