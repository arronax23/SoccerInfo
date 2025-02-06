using AutoMapper;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.Application.Queries.GetPlayersByStats.GetPlayersByStatsQuery.PlayerStatsFilterDto;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;
internal class QueryMapper(IMapper mapper)
{
    public IEnumerable<StatsPlayerBaseDto> Map(IEnumerable<Player> query, CriteriaType criteria)
    {
        return criteria switch
        {
            CriteriaType.Value => MapToEnumerableOf<StatsPlayerMarketValueDto>(query),
            CriteriaType.Goals => MapToEnumerableOf<StatsPlayerGoalsDto>(query),
            CriteriaType.Assists => MapToEnumerableOf<StatsPlayerAssistsDto>(query),
            CriteriaType.GoalsAndAssists => MapToEnumerableOf<StatsPlayerGoalsAndAssistsDto>(query),
            CriteriaType.CleanSheets => MapToEnumerableOf<StatsPlayerCleanSheetsDto>(query),
            CriteriaType.GoalsConceded => MapToEnumerableOf<StatsPlayerGoalsConcededDto>(query),
            CriteriaType.Height => MapToEnumerableOf<StatsPlayerHeightDto>(query),
            CriteriaType.Age => MapToEnumerableOf<StatsPlayerAgeDto>(query),
            CriteriaType.ContractExpiration => MapToEnumerableOf<StatsPlayerContractExpirationDto>(query),
            _ => throw new Exception("Not valid criteria")
        };
    }

    private IEnumerable<TDestinationItem> MapToEnumerableOf<TDestinationItem>(IEnumerable<Player> query)
        where TDestinationItem : StatsPlayerBaseDto
    {
        return mapper.Map<IEnumerable<TDestinationItem>>(query);
    }
}
