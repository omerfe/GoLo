using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(x => x.GameName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.GameRequirements)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Developer)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Publisher)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.TrailerUrl)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(x => x.ImagePath)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}