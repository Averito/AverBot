using AverBot.Core.Domain.Entities;
using AverBot.Core.Domain.Enums;
using AverBot.Core.DTO;
using AverBot.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Services;

public class ConfigurationPunishmentService
{
    public async Task<List<ConfigurationPunishment>> GetByTypeOfViolationAndConfigurationId(TypeOfViolation typeOfViolation, int configurationId)
    {
        await using var ctx = new AverBotContext();

        var punishments = await ctx.ConfigurationPunishments
            .Where(punishment => punishment.ConfigurationId == configurationId && punishment.TypeOfViolation == typeOfViolation)
            .ToListAsync();

        return punishments;
    }

    public async Task<ConfigurationPunishment> Create(CreatePunishmentDTO createPunishmentDto, int configurationId)
    {
        await using var ctx = new AverBotContext();

        var punishment = new ConfigurationPunishment(createPunishmentDto, configurationId);
        var entityEntry = await ctx.ConfigurationPunishments.AddAsync(punishment);

        await ctx.SaveChangesAsync();

        return entityEntry.Entity;
    }
}