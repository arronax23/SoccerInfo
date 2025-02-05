using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayersByStatsFilter;

public class GetPlayersByStatsFilterQuery : IQuery<IEnumerable<PlayerOverviewDto>>
{
    public PlayerStatsFilterDto Filter { get; set; } = null!;

    public class PlayerStatsFilterDto
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
}

