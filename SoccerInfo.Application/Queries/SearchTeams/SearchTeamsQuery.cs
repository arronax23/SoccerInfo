using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.SearchTeams;
public class SearchTeamsQuery : IQuery<IEnumerable<TeamOverviewDto>>
{
    public string Keyword { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
