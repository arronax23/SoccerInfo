namespace SoccerInfo.API.Controllers.Stats.Requests;

public class PlayerStatsFilterRequest
{
    public CriteriaType Criteria { get; set; }
    public IEnumerable<string>? Nationalities { get; set; }
    public IEnumerable<string>? Positions { get; set; }
    public bool IsSortDescending { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }

    public enum CriteriaType
    {
        Value,
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
