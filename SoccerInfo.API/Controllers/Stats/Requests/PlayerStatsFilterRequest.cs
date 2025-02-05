namespace SoccerInfo.API.Controllers.Stats.Requests;

public class PlayerStatsFilterRequest
{
    public CriteriaType Criteria { get; set; }
    public IEnumerable<string>? Nationalities { get; set; }
    public IEnumerable<string>? Positions { get; set; }

    public enum CriteriaType
    {
        Value,
        Goals,
        Assists,
        GoalsAndAssists,
        Height,
        ContractExpiration
    }
}
