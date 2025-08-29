using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;

public class GetLeagueQuery : IQuery<LeagueDto>
{
    public int LeagueId { get; set; }
}
