using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasMany(e => e.Nationalities)
            .WithMany(e => e.Players)
            .UsingEntity(
                "NationalityPlayer",
                l => l.HasOne(typeof(Nationality)).WithMany().HasForeignKey("NationalityId").HasPrincipalKey(nameof(Nationality.Id)),
                r => r.HasOne(typeof(Player)).WithMany().HasForeignKey("PlayerId").HasPrincipalKey(nameof(Player.Id)),
                j => j.HasKey("NationalityId", "PlayerId"));


        builder
            .HasOne(x => x.Characteristics)
            .WithOne();

        builder
            .HasOne(x => x.GeneralPosition)
            .WithMany();

        builder
            .Property(p => p.MarketValueNormalized)
            .HasComputedColumnSql(@"
                CASE 
                    WHEN MarketValueUnit IS NULL OR MarketValue IS NULL THEN NULL
                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000
                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000
                    ELSE NULL
                END", stored: true);
    }
}