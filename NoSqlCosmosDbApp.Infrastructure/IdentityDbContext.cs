using Microsoft.EntityFrameworkCore;
using NoSqlCosmosDbApp.Domain.Models;

namespace NoSqlCosmosDbApp.Infrastructure;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<WorkPlace> WorkPlaces { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAutoscaleThroughput(1000);

        modelBuilder.Entity<User>()
            .HasNoDiscriminator()
            .ToContainer("Users")
            .HasPartitionKey(x => x.Id)
            .HasKey(x => x.Id);

        modelBuilder.Entity<Group>()
            .HasNoDiscriminator()
            .ToContainer("Groups")
            .HasPartitionKey(x => x.Id)
            .HasKey(x => x.Id);

        modelBuilder.Entity<WorkPlace>()
            .HasNoDiscriminator()
            .ToContainer("WorkPlaces")
            .HasPartitionKey(x => x.Id)
            .HasKey(x => x.Id);
    }
}