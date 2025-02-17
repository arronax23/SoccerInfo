using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.Stats;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
{
    public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
    {
        builder.OwnsOne(x => x.Age, y => y.HasIndex(o => o.TotalDays));
        builder.OwnsOne(x => x.ContractPeriod, y => y.HasIndex(o => o.TotalDays));

        builder
            .HasOne(x => x.Player)
            .WithOne(y => y.Stats)
            .HasForeignKey<PlayerStatistic>(x => x.PlayerId);

        builder.HasIndex(x => x.TotalGoals);
        builder.HasIndex(x => x.TotalAssists);
        builder.HasIndex(x => x.TotalGoalsAndAssists);
        builder.HasIndex(x => x.MarketValueNormalized);
        builder.HasIndex(x => x.TotalCleanSheets);
        builder.HasIndex(x => x.TotalGoalsConceded);
        builder.HasIndex(x => x.Height);


    }
}