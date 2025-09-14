using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace NoSqlCosmosDbApp.Infrastructure;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ...
    }
}