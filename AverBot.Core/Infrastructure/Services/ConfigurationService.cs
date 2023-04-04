using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Services;

public class ConfigurationService
{
    public async Task<Configuration> Create(int serverId)
    {
        await using var ctx = new AverBotContext();

        var newConfiguration = new Configuration(serverId);
        var createdConfiguration = await ctx.Configurations.AddAsync(newConfiguration);

        await ctx.SaveChangesAsync();

        return createdConfiguration.Entity;
    }
}