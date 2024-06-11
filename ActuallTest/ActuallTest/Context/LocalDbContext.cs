using Microsoft.EntityFrameworkCore;

namespace TestPrep2.Context;

public class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
        
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}