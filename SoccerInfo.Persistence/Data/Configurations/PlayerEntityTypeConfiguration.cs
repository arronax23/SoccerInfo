using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models;
namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasMany(e => e.NationalityImages)
            .WithMany(e => e.Players)
            .UsingEntity(
                "NationalityImagePlayer",
                l => l.HasOne(typeof(NationalityImage)).WithMany().HasForeignKey("NationalityImageId").HasPrincipalKey(nameof(NationalityImage.Id)),
                r => r.HasOne(typeof(Player)).WithMany().HasForeignKey("PlayerId").HasPrincipalKey(nameof(Player.Id)),
                j => j.HasKey("NationalityImageId", "PlayerId"));

    }
}
