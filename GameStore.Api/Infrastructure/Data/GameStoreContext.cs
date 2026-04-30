using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .OwnsOne(g => g.Price, price =>
            {
                price.Property(p => p.Amount)
                     .HasColumnName("Price")
                     .HasPrecision(18, 2);

                price.Property(p => p.Amount).IsRequired();

                price.Property(p => p.Currency)
                     .HasColumnName("Currency")
                     .HasMaxLength(3)
                     .IsRequired();
            });

        modelBuilder.Entity<Game>()
            .OwnsOne(g => g.Title, title =>
            {
                title.Property(t => t.Value)
                     .HasColumnName("Title")
                     .HasMaxLength(100)
                     .IsRequired();
            });

        modelBuilder.Entity<Game>()
            .OwnsOne(g => g.ReleaseDate, releaseDate =>
            {
                releaseDate.Property(rd => rd.Value)
                          .HasColumnName("ReleaseDate")
                          .IsRequired();
            });

        modelBuilder.Entity<Game>().Navigation(g => g.Price).IsRequired();
        modelBuilder.Entity<Game>().Navigation(g => g.Title).IsRequired();
        modelBuilder.Entity<Game>().Navigation(g => g.ReleaseDate).IsRequired();
    }
}
