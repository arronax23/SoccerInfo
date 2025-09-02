using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeagueTeams;

public class GetLeagueTeamsQuery : IQuery<LeagueTeamsDto?>
{
    public int LeagueId { get; set; }
}
