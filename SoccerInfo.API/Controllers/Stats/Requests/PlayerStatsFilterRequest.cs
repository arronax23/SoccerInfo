namespace SoccerInfo.API.Controllers.Stats.Requests;

public class PlayerStatsFilterRequest
{
    public CriteriaType Criteria { get; set; }
    public IEnumerable<string>? Nationalities { get; set; }
    public IEnumerable<string>? Positions { get; set; }
    public IEnumerable<string>? Teams { get; set; }
    public IEnumerable<string>? Leagues { get; set; }
    public bool IsSortDescending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public enum CriteriaType
    {
        MarketValue,
        MarketValueProgress,
        Goals,
        Assists,
        GoalsAndAssists,
        CleanSheets,
        GoalsConceded,
        Height,
        Age,
        ContractExpiration
    }
}
