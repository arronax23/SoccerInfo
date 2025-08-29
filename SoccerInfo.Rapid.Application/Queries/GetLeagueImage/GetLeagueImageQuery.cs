using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;

public class GetLeagueImageQuery : IQuery<string>
{
    public int LeagueId { get; set; }
}
