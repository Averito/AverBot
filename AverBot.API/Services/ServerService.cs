﻿using AverBot.API.Constants;
using AverBot.API.Context;
using AverBot.API.DTO;
using AverBot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AverBot.API.Services;

public class ServerService
{
    public async Task<Server> Create(CreateServerDTO createServerDto, int userId)
    {
        await using var ctx = new AverBotContext();

        var server = await ctx.Servers.FirstOrDefaultAsync(server => server.DiscordId == createServerDto.DiscordId);
        if (server != null) throw new BadHttpRequestException(ExceptionMessage.ServerFound);

        var newServer = new Server(createServerDto.DiscordId, createServerDto.Name, userId);
        var createdServer = await ctx.Servers.AddAsync(newServer);
        await ctx.SaveChangesAsync();

        return createdServer.Entity;
    }
}