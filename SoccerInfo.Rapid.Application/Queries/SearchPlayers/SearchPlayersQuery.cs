using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.SearchPlayers;

public class SearchPlayersQuery : IQuery<IEnumerable<PlayerOverviewDto>?>
{
    public string Keyword { get; set; } = null!;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
