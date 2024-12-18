using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeams;

public class GetTeamsQuery : IQuery<IEnumerable<TeamDto>>
{
    public int LeagueId { get; set; }
}

