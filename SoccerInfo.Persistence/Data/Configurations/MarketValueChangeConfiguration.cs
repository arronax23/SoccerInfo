using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Domain.Models;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class MarketValueChangeConfiguration : IEntityTypeConfiguration<MarketValueChange>
{
    public void Configure(EntityTypeBuilder<MarketValueChange> builder)
    {
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