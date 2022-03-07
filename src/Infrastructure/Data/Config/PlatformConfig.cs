using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PlatformConfig : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.Property(x => x.PlatformName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LogoPath)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}