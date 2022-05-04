using Microsoft.EntityFrameworkCore;

namespace watchify.Shared;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration config;
    
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