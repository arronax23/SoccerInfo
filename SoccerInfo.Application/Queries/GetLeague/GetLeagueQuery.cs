using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeague;

public class GetLeagueQuery : IQuery<LeagueDto>
{
    public int LeagueId { get; set; }
}

