using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Context;

public class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Client_Trip> ClientTrips { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Country_Trip> CountryTrips { get; set; }
    public DbSet<Country> Countries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country_Trip>(entity =>
        {
            entity.HasKey(ct => new { ct.IdTrip, ct.IdCountry });
            entity.HasOne(ct => ct.Country)
                .WithMany(c => c.CountryTrips)
                .HasForeignKey(ct => ct.IdCountry)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(ct => ct.Trip)
                .WithMany(t => t.CountryTrips)
                .HasForeignKey(ct => ct.IdTrip)
                .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<Client_Trip>(entity =>
        {
            entity.HasKey(ct => new { ct.IdTrip, ct.IdClient });
            entity.HasOne(ct => ct.Client)
                .WithMany(c => c.ClientTrips)
                .HasForeignKey(ct => ct.IdClient)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(ct => ct.Trip)
                .WithMany(t => t.ClientTrips)
                .HasForeignKey(ct => ct.IdTrip)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}