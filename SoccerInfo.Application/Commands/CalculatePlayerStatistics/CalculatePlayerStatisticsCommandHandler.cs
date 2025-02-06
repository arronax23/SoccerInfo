using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NodaTime;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.Data.Models.Stats;
using SoccerInfo.Persistence.Transactions;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.CalculatePlayerStatistics;

internal class CalculatePlayerStatisticsCommandHandler(IConfiguration configuration, ApplicationDbContext dbContext)
    : ICommandHandler<CalculatePlayerStatisticsCommand>
{
    public async Task Handle(CalculatePlayerStatisticsCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();
        var statistics = new List<PlayerStatistic>();

        var dbPlayers = dbContext.Players
            .Include(p => p.Characteristics)
            .ThenInclude(c => c.OutfieldPlayerStats)
            .Include(p => p.Characteristics)
            .ThenInclude(c => c.GoalKeeperStats)
            .AsNoTracking();

        foreach (var dbPlayer in dbPlayers)
        {
            var stats = new PlayerStatistic()
            {
                Age = CalculateAge(dbPlayer),
                TotalGoals = CalculateTotalGoals(dbPlayer),
                TotalAssists = CalculateTotalAssists(dbPlayer),
                TotalGoalsAndAssists = CalculateTotalGoalsAndAssists(dbPlayer),
                TotalGoalsConceded = CalculateTotalGoalsConceded(dbPlayer),
                TotalCleanSheets = CalculateTotalCleanSheets(dbPlayer),
                Height = dbPlayer.Characteristics?.Height,
                MarketValue = dbPlayer.MarketValue,
                MarketValueUnit = dbPlayer.MarketValueUnit,
                MarketValueNormalized = dbPlayer.MarketValueNormalized,
                ContractPeriod = CalculateContractPeriod(dbPlayer),
                LastMarkeValueProgress = CalculateLastMarkeValueProgress(dbPlayer),
                PlayerId = dbPlayer.Id,
            };

            var dbStats = dbContext
                .PlayerStatistics
                .AsNoTracking()
                .SingleOrDefault(s => s.PlayerId == dbPlayer.Id);

            if (dbStats == null)
            {
                dbContext.PlayerStatistics.Add(stats);
            }
            else
            {
                TrackEntity(stats, dbStats.Id);
                dbContext.PlayerStatistics.Update(stats);
            }
        }

        dbContext.ChangeTracker.ShowEntries();

        await dbContext.SaveChangesAsync();
        await transaction.ResolveAsync(configuration);

    }

    private int? CalculateTotalGoals(Player player)
    {
        if (!player.ContainsOutfieldPlayerStats())
            return null;

        return player.Characteristics!.OutfieldPlayerStats!.Sum(s => s.Goals ?? 0);
    }

    private int? CalculateTotalAssists(Player player)
    {
        if (!player.ContainsOutfieldPlayerStats())
            return null;

        return player.Characteristics!.OutfieldPlayerStats!.Sum(s => s.Assists ?? 0);
    }

    private int? CalculateTotalGoalsAndAssists(Player player)
    {
        return CalculateTotalGoals(player) + CalculateTotalAssists(player);
    }

    private int? CalculateTotalGoalsConceded(Player player)
    {
        if (!player.ContainsGoalKeeperStats())
            return null;

        return player.Characteristics!.GoalKeeperStats!.Sum(s => s.GoalsConceded ?? 0);
    }

    private int? CalculateTotalCleanSheets(Player player)
    {
        if (!player.ContainsGoalKeeperStats())
            return null;

        return player.Characteristics!.GoalKeeperStats!.Sum(s => s.CleanSheets ?? 0);
    }


    private DateRange CalculateAge(Player player)
    {
        var period = Period.Between(
            LocalDateTime.FromDateTime(player.DateOfBirth),
            LocalDateTime.FromDateTime(DateTime.Today));

        return GetDateRangeFromPeriod(period);
    }



    private DateRange? CalculateContractPeriod(Player player)
    {
        if (player.Characteristics?.ContractExpirationDate is null)
            return null;

        var period = Period.Between(
            LocalDateTime.FromDateTime(DateTime.Today), 
            LocalDateTime.FromDateTime(player.Characteristics.ContractExpirationDate.Value));

        return GetDateRangeFromPeriod(period);
    }

    private DateRange GetDateRangeFromPeriod(Period period)
    {
        return new DateRange 
        { 
            Years = period.Years, 
            Months = period.Months, 
            Days = period.Days, 
            TotalDays = GetTotalDaysFromPeriod(period) 
        };
    }

    private int GetTotalDaysFromPeriod(Period period)
    {
        return Period.Between(LocalDate.MinIsoValue, LocalDate.MinIsoValue.Plus(period), PeriodUnits.Days).Days;
    }

    private float? CalculateLastMarkeValueProgress(Player player)
    {
        var marketValueProgress = player.MarketValueProgress;

        if (marketValueProgress is null || marketValueProgress.Count < 2)
            return null;

        var orderedMarketValueProgress = marketValueProgress.OrderByDescending(m => m.ChangeDate);

        return (orderedMarketValueProgress.ElementAt(0).MarketValueNormalized - orderedMarketValueProgress.ElementAt(1).MarketValueNormalized);
    }

    private void TrackEntity(PlayerStatistic stats, int statsId)
    {
        stats.Id = statsId;
        dbContext.Entry(stats).State = EntityState.Modified;
        dbContext.Entry(stats).Reference(s => s.Age).TargetEntry!.State = EntityState.Modified;

        if (stats.ContractPeriod is not null)
            dbContext.Entry(stats).Reference(s => s.ContractPeriod).TargetEntry!.State = EntityState.Modified;
    }
}
