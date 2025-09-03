using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Domain.Models.Extraction;

namespace SoccerInfo.Persistence.Data.Configurations;

internal sealed class ExtractionInfoConfiguration : IEntityTypeConfiguration<ExtractionInfo>
{
    public void Configure(EntityTypeBuilder<ExtractionInfo> builder)
    {
        builder.ToTable("ExtractionInfos", "extraction");
    }
}