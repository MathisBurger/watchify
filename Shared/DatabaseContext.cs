using Microsoft.EntityFrameworkCore;
using watchify.Models.Database;

namespace watchify.Shared;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration config;
    
    public DbSet<User> Users { get; set; }
    
    public DatabaseContext(IConfiguration config)
    {
        this.config = config;

        // TODO: Remove in production
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = config.GetValue<string>("DB:connString");
        optionsBuilder.UseNpgsql(connectionString);



        base.OnConfiguring(optionsBuilder);
    }
    
}