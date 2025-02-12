using SoccerInfo.Persistence.Data.Models;
using System.Linq.Expressions;
using static SoccerInfo.Application.Queries.GetPlayersByStats.GetPlayersByStatsQuery;
using static SoccerInfo.Application.Queries.GetPlayersByStats.GetPlayersByStatsQuery.PlayerStatsFilterDto;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;

internal static class QueryFiltering
{
    public static IQueryable<Player> ApplyFilter(this IQueryable<Player> query, PlayerStatsFilterDto filter)
    {
        if (filter.Positions is not null && filter.Positions.Any())
            query = query.Where(p => filter.Positions.Contains(p.Position));

        if (filter.Nationalities is not null && filter.Nationalities.Any())
            query = query.Where(p => p.Nationalities.Any(n => filter.Nationalities.Contains(n.Country)));

        query = filter.Criteria switch
        {
            CriteriaType.Value => query
                .Where(p => p.Stats.MarketValueNormalized != null)
                .Sort(p => p.Stats.MarketValueNormalized, filter.IsSortDescending),

            CriteriaType.Goals => query
                .Where(p => p.Stats.TotalGoals != null)
                .Sort(p => p.Stats.TotalGoals, filter.IsSortDescending),

            CriteriaType.Assists => query
                .Where(p => p.Stats.TotalAssists != null)
                .Sort(p => p.Stats.TotalAssists, filter.IsSortDescending),

            CriteriaType.GoalsAndAssists => query
                .Where(p => p.Stats.TotalGoalsAndAssists != null)
                .Sort(p => p.Stats.TotalGoalsAndAssists, filter.IsSortDescending),

            CriteriaType.CleanSheets => query
                .Where(p => p.Stats.TotalCleanSheets != null)
                .Sort(p => p.Stats.TotalCleanSheets, filter.IsSortDescending),

            CriteriaType.GoalsConceded => query
                .Where(p => p.Stats.TotalGoalsConceded != null)
                .Sort(p => p.Stats.TotalGoalsConceded, filter.IsSortDescending),

            CriteriaType.Height => query
                .Where(p => p.Stats.Height != null)
                .Sort(p => p.Stats.Height, filter.IsSortDescending),

            CriteriaType.Age => query
                .Where(p => p.Stats.Age != null)
                .Sort(p => p.Stats.Age!.TotalDays, filter.IsSortDescending),

            CriteriaType.ContractExpiration => query
                .Where(p => p.Stats.ContractPeriod != null)
                .Sort(p => p.Stats.ContractPeriod!.TotalDays, filter.IsSortDescending),

            _ => query
        };

        return query.ApplyPagination(filter.PageNumber, filter.PageSize);
    }

    private static IQueryable<Player> ApplyPagination(this IQueryable<Player> query, int pageNumber, int pageSize)
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    private static IQueryable<Player> Sort<TKey>(
        this IQueryable<Player> query,
        Expression<Func<Player, TKey>> keySelector,
        bool descending)
    {

        if (descending)
            return query.OrderByDescending(keySelector);
        else
            return query.OrderBy(keySelector);
    }
}

