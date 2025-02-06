using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.Stats;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
{
    public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
    {
        builder.OwnsOne(x => x.Age);
        builder.OwnsOne(x => x.ContractPeriod);

        builder
            .HasOne(x => x.Player)
            .WithOne(y => y.Stats)
            .HasForeignKey<PlayerStatistic>(x => x.PlayerId);
    }
}