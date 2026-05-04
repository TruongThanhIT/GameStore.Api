using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Infrastructure.Configruations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
               .ValueGeneratedOnAdd();

        builder.HasOne(g => g.Genre)
               .WithMany()
               .HasForeignKey(g => g.GenreId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(g => g.Title, title =>
        {
            title.Property(t => t.Value)
                 .HasColumnName("Title")
                 .HasMaxLength(100)
                 .IsRequired();
        });

        builder.OwnsOne(g => g.ReleaseDate, releaseDate =>
        {
            releaseDate.Property(rd => rd.Value)
                       .HasColumnName("ReleaseDate")
                       .IsRequired();
        });

        builder.OwnsOne(g => g.Price, price =>
        {
            price.Property(p => p.Amount)
                 .HasColumnName("Price")
                 .HasPrecision(18, 2)
                 .IsRequired();

            price.Property(p => p.Currency)
                 .HasColumnName("Currency")
                 .HasMaxLength(3)
                 .IsRequired();
        });

        builder.Navigation(g => g.Title).IsRequired();
        builder.Navigation(g => g.ReleaseDate).IsRequired();
        builder.Navigation(g => g.Price).IsRequired();
    }
}