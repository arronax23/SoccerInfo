using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;

public class GetPlayersByStatsQuery : IQuery<IEnumerable<StatsPlayerBaseDto>>
{
    public PlayerStatsFilterDto Filter { get; set; } = null!;

    public class PlayerStatsFilterDto
    {
        public CriteriaType Criteria { get; set; }
        public IEnumerable<string>? Nationalities { get; set; }
        public IEnumerable<string>? Positions { get; set; }
        public bool IsSortDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

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
}

