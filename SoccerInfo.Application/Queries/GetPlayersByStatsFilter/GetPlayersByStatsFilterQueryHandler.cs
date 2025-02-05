using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using System.Linq;
using System.Linq.Expressions;
using static SoccerInfo.Application.Queries.GetPlayersByStatsFilter.GetPlayersByStatsFilterQuery;
using static SoccerInfo.Application.Queries.GetPlayersByStatsFilter.GetPlayersByStatsFilterQuery.PlayerStatsFilterDto;

namespace SoccerInfo.Application.Queries.GetPlayersByStatsFilter;

internal class GetPlayersByStatsFilterQueryHandler(ApplicationDbContext dbContext) 
    : IQueryHandler<GetPlayersByStatsFilterQuery, IEnumerable<PlayerOverviewDto>>
{
    public Task<IEnumerable<PlayerOverviewDto>> Handle(GetPlayersByStatsFilterQuery request, CancellationToken cancellationToken)
    {
        //dbContext.Players


        throw new NotImplementedException();    
    }

    private IQueryable<Player> ApplyFilter(IQueryable<Player> query, PlayerStatsFilterDto filter)
    {
        if (filter.Positions is not null && filter.Positions.Any()) 
            query = query.Where(p => filter.Positions.Contains(p.Position));

        if (filter.Nationalities is not null && filter.Nationalities.Any())
            query = query.Where(p => p.Nationalities.Any(n => filter.Nationalities.Contains(n.Country)));


        switch (filter.Criteria)
        {
            case CriteriaType.Value:
                break;
            case CriteriaType.Goals:
                break;
            case CriteriaType.Assists:
                break;
            case CriteriaType.GoalsAndAssists:
                break;
            case CriteriaType.Height:
                break;
            case CriteriaType.ContractExpiration:
                break;
            default:
                break;
        }

        return query;
    }
    //public IQueryable<Player> ApplyOrdering(IQueryable<Player> query, CriteriaType criteria, bool descending)
    //{
    //    if (descending)
    //    {
    //        return query.OrderByDescending(p => p.MarketValue);
    //    }
    //    else
    //    {
    //        return query.OrderBy();
    //    }
    //}
}
