using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Infrastructure.Configruations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
               .ValueGeneratedOnAdd();

        builder.Property(g => g.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasIndex(g => g.Name)
               .IsUnique();
    }
}