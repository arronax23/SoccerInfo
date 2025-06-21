using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NodaTime;
using SoccerInfo.Application.Intrefaces;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Stats;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Domain.Repositories.Generic;

namespace SoccerInfo.Application.Commands.CalculatePlayerStatistics;

internal class CalculatePlayerStatisticsCommandHandler(
    IConfiguration configuration, 
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository,
    IGenericRepository<PlayerStatistic> statsRepository)
    : ICommandHandler<CalculatePlayerStatisticsCommand>
{
    public async Task Handle(CalculatePlayerStatisticsCommand request, CancellationToken cancellationToken)
    {
        using var transaction = unitOfWork.BeginTransaction();
        var statistics = new List<PlayerStatistic>();

        var dbPlayers = playerRepository.ToQuery().AsNoTracking();

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

            var dbStats = await statsRepository
                .ToQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.PlayerId == dbPlayer.Id);

            if (dbStats == null)
            {
                await statsRepository.AddAsync(stats);
            }
            else
            {
                TrackEntity(stats, dbStats.Id);
                statsRepository.Update(stats);
            }
        }

        unitOfWork.ShowEntires();

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.ResolveTransactionAsync(transaction);

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
        var referenceDate = LocalDate.FromDateTime(DateTime.Today);
        return Period.Between(referenceDate, referenceDate.Plus(period), PeriodUnits.Days).Days;
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
        unitOfWork.Entry(stats).State = EntityState.Modified;
        unitOfWork.Entry(stats).Reference(s => s.Age).TargetEntry!.State = EntityState.Modified;

        if (stats.ContractPeriod is not null)
            unitOfWork.Entry(stats).Reference(s => s.ContractPeriod).TargetEntry!.State = EntityState.Modified;
    }
}
