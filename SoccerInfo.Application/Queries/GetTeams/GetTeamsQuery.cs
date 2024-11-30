using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.GetTeams;

internal class GetTeamsQuery : IQuery<IEnumerable<TeamDto>>
{
}

