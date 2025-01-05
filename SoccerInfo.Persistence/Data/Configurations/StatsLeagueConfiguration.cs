using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class StatsLeagueConfiguration : IEntityTypeConfiguration<StatsLeague>
{
    public void Configure(EntityTypeBuilder<StatsLeague> builder)
    {
        builder.ToTable("StatsLeagues");

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Base64Image)
            .HasMaxLength(-1);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.OutfieldPlayerStats)
            .WithOne(y => y.League)
            .HasForeignKey(y => y.LeagueId);

        builder.HasMany(x => x.GoalKeeperStats)
            .WithOne(y => y.League)
            .HasForeignKey(y => y.LeagueId);
    }
}