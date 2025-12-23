using Microsoft.EntityFrameworkCore;
using Train.Models.Entity;

namespace Train.Models.Repository;

public class TrainDbContext : DbContext
{
    public TrainDbContext()
    {
    }

    public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options)
    {
    }

    public DbSet<Train.Models.Entity.Train> Trains { get; set; }
    public DbSet<Compagnie> Compagnies { get; set; }
    public DbSet<Voyage> Voyages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");

        // Train
        modelBuilder.Entity<Train.Models.Entity.Train>()
            .HasKey(t => t.IdTrain);

        modelBuilder.Entity<Train.Models.Entity.Train>()
            .HasOne(t => t.CompagnieNav)
            .WithMany(c => c.Trains)
            .HasForeignKey(t => t.IdCompagnie)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Train.Models.Entity.Train>()
            .HasIndex(t => t.Nom)
            .IsUnique();

        // Compagnie
        modelBuilder.Entity<Compagnie>()
            .HasKey(c => c.IdCompagnie);

        modelBuilder.Entity<Compagnie>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Compagnie>()
            .HasIndex(c => c.Nom);

        // Voyage
        modelBuilder.Entity<Voyage>()
            .HasKey(v => v.IdVoyage);

        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.TrainNav)
            .WithMany(t => t.Voyages)
            .HasForeignKey(v => v.IdTrain)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Voyage>()
            .HasIndex(v => new { v.IdTrain, v.DateDepart });

        modelBuilder.Entity<Voyage>()
            .Property(v => v.Prix)
            .HasPrecision(10, 2);
    }
}