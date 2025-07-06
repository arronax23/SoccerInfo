using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Domain.Models.Transfers;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.OwnsOne(x => x.From);
        builder.OwnsOne(x => x.To);
        builder.OwnsOne(x => x.Fee);
        builder.OwnsOne(x => x.MarketValue);
    }
}