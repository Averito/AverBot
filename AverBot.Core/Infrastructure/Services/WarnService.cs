﻿using AverBot.Core.Infrastructure.Context;
using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Services;

public class WarnService
{
    public async Task<List<Warn>> GetWarnsByGuildUserIdAndServerId(int guildUserId, int serverId)
    {
        await using var ctx = new AverBotContext();

        var warns = await ctx.Warns
            .Where(warn => warn.GuildUserId == guildUserId && warn.ServerId == serverId)
            .ToListAsync();

        return warns;
    }
    
    public async Task<List<Warn>> GetWarnsByGuildUserDiscordIdAndServerDiscordId(ulong guildUserDiscordId, ulong serverDiscordId)
    {
        await using var ctx = new AverBotContext();

        var warns = await ctx.Warns
            .Include(warn => warn.GuildUser)
            .Include(warn => warn.Server)
            .Where(warn => warn.GuildUser.DiscordId == guildUserDiscordId && warn.Server.DiscordId == serverDiscordId)
            .ToListAsync();

        return warns;
    }
    
    public async Task<Warn> Create(IHubCallerClients hubCallerClients, CreateWarnDTO createWarnDto)
    {
        await using var ctx = new AverBotContext();

        var newWarn = new Warn(createWarnDto);
        var createdWarn = await ctx.Warns.AddAsync(newWarn);
        await ctx.SaveChangesAsync();

        await hubCallerClients.Others.SendAsync("WarnCreated", createdWarn.Entity);
        
        return createdWarn.Entity;
    }
    public async Task<Warn> Create(CreateWarnDTO createWarnDto)
    {
        await using var ctx = new AverBotContext();

        var newWarn = new Warn(createWarnDto);
        var createdWarn = await ctx.Warns.AddAsync(newWarn);
        await ctx.SaveChangesAsync();

        return createdWarn.Entity;
    }
}
