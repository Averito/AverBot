﻿using AverBot.API.Context;
using AverBot.API.DTO;
using AverBot.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AverBot.API.Services;

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
    
    public async Task<Warn> Create(IHubCallerClients hubCallerClients, CreateWarnDTO createWarnDto)
    {
        await using var ctx = new AverBotContext();

        var newWarn = new Warn(createWarnDto);
        var createdWarn = await ctx.Warns.AddAsync(newWarn);
        await ctx.SaveChangesAsync();

        await hubCallerClients.Others.SendAsync("WarnCreated", createdWarn.Entity);
        
        return createdWarn.Entity;
    }
}
