using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.Infrastructure.Context;
using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Services;

public class GuildUserService
{
    public async Task<GuildUser> GetByDiscordId(ulong discordId)
    {
        await using var ctx = new AverBotContext();

        var guildUser = await ctx.GuildUsers.FirstOrDefaultAsync(guildUser => guildUser.DiscordId == discordId);
        if (guildUser == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

        return guildUser;
    }
    public async Task<GuildUser> Create(CreateGuildUserDTO createGuildUserDto, int serverId)
    {
        await using var ctx = new AverBotContext();

        var guildUser =
            await ctx.GuildUsers.FirstOrDefaultAsync(guildUser => guildUser.DiscordId == createGuildUserDto.DiscordId);
        if (guildUser != null) throw new BadHttpRequestException(ExceptionMessage.GuildUserFound);
        
        var newGuildUser = new GuildUser(createGuildUserDto);
        var createdGuildUser = await ctx.GuildUsers.AddAsync(newGuildUser);
        await ctx.SaveChangesAsync();

        await ctx.ServerGuildUsers.AddAsync(new ServerGuildUser(serverId, createdGuildUser.Entity.Id));
        await ctx.SaveChangesAsync();

        return createdGuildUser.Entity;
    }

    public async Task<GuildUser> AddToServer(AddToServerDTO addToServerDto)
    {
        await using var ctx = new AverBotContext();
        
        var guildUser =
            await ctx.GuildUsers.FirstOrDefaultAsync(guildUser => guildUser.DiscordId == addToServerDto.GuildUserDiscordId);
        if (guildUser == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

        await ctx.ServerGuildUsers.AddAsync(new ServerGuildUser(addToServerDto.ServerId, guildUser.Id));
        await ctx.SaveChangesAsync();

        return guildUser;
    }
}