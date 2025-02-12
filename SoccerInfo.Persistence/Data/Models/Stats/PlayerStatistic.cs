using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models.Stats;

public class PlayerStatistic : IEntity
{
    public int Id { get; set; }
    public DateRange Age { get; set; } = null!;
    public int? TotalGoals { get; set; }
    public int? TotalGoalsAndAssists { get; set; }
    public int? TotalAssists { get; set; }
    public int? TotalGoalsConceded { get; set; }
    public int? TotalCleanSheets { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; }
    public float? MarketValueNormalized { get; set; }
    public float? LastMarkeValueProgress { get; set; }
    public float? Height { get; set; }
    public DateRange? ContractPeriod { get; set; }
    public int PlayerId { get; set; } 
    public virtual Player Player { get; set; } = null!;

}

public class DateRange
{
    public int Years { get; set; }
    public int Months { get; set; }
    public int Days { get; set; }
    public int TotalDays { get; set; }
}
