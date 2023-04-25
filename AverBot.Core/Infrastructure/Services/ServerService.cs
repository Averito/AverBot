using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.Infrastructure.Context;
using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AverBot.Core.Infrastructure.Services;

public class ServerService
{
    public async Task<Server> GetById(int id)
    {
        await using var ctx = new AverBotContext();

        var server = await ctx.Servers
            .Include(server => server.User)
            .Include(server => server.Warns)
            .Include(server => server.Configuration)
            .FirstOrDefaultAsync(server => server.Id == id);
        if (server == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

        return server;
    }
    public async Task<Server> GetByDiscordId(ulong discordId)
    {
        await using var ctx = new AverBotContext();

        var server = await ctx.Servers
            .Include(server => server.User)
            .Include(server => server.Warns)
            .Include(server => server.Configuration)
            .FirstOrDefaultAsync(server => server.DiscordId == discordId);
        if (server == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

        return server;
    }
    public async Task<Server> Create(CreateServerDTO createServerDto, int userId)
    {
        await using var ctx = new AverBotContext();

        var server = await ctx.Servers.FirstOrDefaultAsync(server => server.DiscordId == createServerDto.DiscordId);
        if (server != null) throw new BadHttpRequestException(ExceptionMessage.ServerFound);

        var newServer = new Server(createServerDto, userId);
        var createdServer = await ctx.Servers.AddAsync(newServer);
        await ctx.SaveChangesAsync();

        return createdServer.Entity;
    }
    
    public int GetCurrentServerIdFromHttpContext(HttpContext httpContext)
    {
        var currentServerIdString = httpContext?.User?.Claims.ToList()[1].Value;
        int currentServerId;

        var canConvert = int.TryParse(currentServerIdString, out currentServerId);
        if (!canConvert) throw new BadHttpRequestException(ExceptionMessage.CurrentServerIdNotValid);

        return currentServerId;
    }
}