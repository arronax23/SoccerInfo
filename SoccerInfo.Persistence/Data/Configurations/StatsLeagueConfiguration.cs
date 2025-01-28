using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using System.Reflection.Emit;

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

        builder.HasMany(x => x.OutfieldPlayerStats)
            .WithOne(y => y.League)
            .HasForeignKey(y => y.LeagueId);

        builder.HasMany(x => x.GoalKeeperStats)
            .WithOne(y => y.League)
            .HasForeignKey(y => y.LeagueId);

        builder.Property<string>("_HashCode")
            .HasComputedColumnSql("CONVERT(varchar(64), HASHBYTES('SHA2_256', CONCAT(Name, Base64Image)), 1)")
            .HasColumnName("_HashCode")
            .IsRequired();

        builder.HasIndex("_HashCode")
            .IsUnique();

    }
}