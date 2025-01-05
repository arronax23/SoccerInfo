using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

namespace SoccerInfo.Persistence.Data.Configurations;
internal sealed class SocialsMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
       => builder.ToTable("Socials");
}