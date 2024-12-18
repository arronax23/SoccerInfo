using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeagues;

public class GetLeaguesQuery : IQuery<IEnumerable<LeagueDto>>
{
}

