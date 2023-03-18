using AverBot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AverBot.API.Context;

public class AverBotContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AverBotContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var host = Environment.GetEnvironmentVariable("POSTGRESQL_HOST");
        var port = Environment.GetEnvironmentVariable("POSTGRESQL_PORT");
        var databaseName = Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE_NAME");
        var username = Environment.GetEnvironmentVariable("POSTGRESQL_USER_NAME");
        var password = Environment.GetEnvironmentVariable("POSTGRESQL_USER_PASSWORD");
        
        optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={databaseName};Username={username};Password={password}");
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(entity => {
            entity.HasIndex(user => user.DiscordId).IsUnique();
        });
        
        base.OnModelCreating(builder);
    }
}