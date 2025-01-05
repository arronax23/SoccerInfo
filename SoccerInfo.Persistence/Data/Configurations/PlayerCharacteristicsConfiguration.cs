using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class PlayerCharacteristicsConfiguration : IEntityTypeConfiguration<PlayerCharacteristic>
{
    public void Configure(EntityTypeBuilder<PlayerCharacteristic> builder)
    {
        builder.ToTable("PlayerCharacteristics");
        builder.OwnsOne(x => x.NationalTeam);
        builder.OwnsOne(x => x.BrithPlace);

        builder.HasMany(x => x.OutfieldPlayerStats)
            .WithOne()
            .HasForeignKey(y => y.PlayerCharacteristicId);

        builder.HasMany(x => x.GoalKeeperStats)
            .WithOne()
            .HasForeignKey(y => y.PlayerCharacteristicId);
    }
}