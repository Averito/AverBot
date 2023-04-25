using AverBot.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Context;

public class AverBotContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<Warn> Warns { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<ConfigurationPunishment> ConfigurationPunishments { get; set; }

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
        builder.Entity<User>(entity =>
        {
            entity.HasIndex(user => user.DiscordId).IsUnique();
        });
        builder.Entity<User>()
            .HasMany(user => user.Servers)
            .WithOne(server => server.User)
            .HasForeignKey(server => server.UserId)
            .IsRequired();

        builder.Entity<Server>(entity =>
        {
            entity.HasIndex(server => server.DiscordId).IsUnique();
        });
        builder.Entity<Server>()
            .HasMany(server => server.Warns)
            .WithOne(warn => warn.Server)
            .HasForeignKey(warn => warn.ServerId)
            .IsRequired();

        builder.Entity<Configuration>()
            .HasIndex(configuration => configuration.ServerId).IsUnique();

        builder.Entity<ConfigurationPunishment>()
            .HasOne(configurationPunishments => configurationPunishments.Configuration)
            .WithMany(configuration => configuration.Punishments)
            .HasForeignKey(configurationPunishments => configurationPunishments.ConfigurationId)
            .IsRequired();

        base.OnModelCreating(builder);
    }
}