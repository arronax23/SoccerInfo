using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Domain.Models.Transfers;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {

        builder.OwnsOne(x => x.Fee);
        builder.OwnsOne(x => x.MarketValue);

        builder.HasOne(t => t.From)
            .WithMany()
            .HasForeignKey(t => t.FromId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.To)
            .WithMany()
            .HasForeignKey(t => t.ToId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}