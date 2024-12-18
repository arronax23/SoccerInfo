
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.SearchPlayers;
public class SearchPlayersQuery : IQuery<IEnumerable<PlayerOverviewDto>>
{
    public string Keyword { get; set; } = string.Empty;
}
