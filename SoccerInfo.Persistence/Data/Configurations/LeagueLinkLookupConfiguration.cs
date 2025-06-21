using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Domain.Models.Extraction;

namespace SoccerInfo.Persistence.Data.Configurations;

internal sealed class LeagueLinkLookupConfiguration : IEntityTypeConfiguration<LeagueLinkLookup>
{
    public void Configure(EntityTypeBuilder<LeagueLinkLookup> builder)
    {
        builder.ToTable("LeagueLinksLookup", "extraction");
    }
}