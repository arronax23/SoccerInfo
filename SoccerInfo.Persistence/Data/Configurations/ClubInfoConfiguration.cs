using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static SoccerInfo.Domain.Models.Transfers.Transfer;

namespace SoccerInfo.Persistence.Data.Configurations;

internal sealed class ClubInfoConfiguration : IEntityTypeConfiguration<ClubInfo>
{
    public void Configure(EntityTypeBuilder<ClubInfo> builder)
    {
        builder
            .HasOne(ci => ci.Club)
            .WithMany() 
            .HasForeignKey(ci => ci.ClubId);

        builder
            .HasOne(ci => ci.Team)
            .WithMany()
            .HasForeignKey(ci => ci.TeamId);
    }
}