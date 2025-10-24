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

        builder.OwnsOne(t => t.From, from =>
        {
            from.HasOne(ci => ci.Club)
                .WithMany()
                .HasForeignKey(ci => ci.ClubId);

            from.HasOne(ci => ci.Team)
                .WithMany()
                .HasForeignKey(ci => ci.TeamId);
        });

        builder.OwnsOne(t => t.To, to =>
        {
            to.HasOne(ci => ci.Club)
                .WithMany()
                .HasForeignKey(ci => ci.ClubId);

            to.HasOne(ci => ci.Team)
                .WithMany()
                .HasForeignKey(ci => ci.TeamId);
        });
    }
}