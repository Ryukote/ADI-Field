using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ADIFieldDbContext : DbContext
{
    public ADIFieldDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<DriverCatalog> DriverCatalog { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<EventDrivers> EventDrivers { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
