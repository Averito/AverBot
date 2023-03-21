using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Handlers;

public class CommandsHandler
{
    private readonly CommandService _commandService;
    private readonly ServiceProvider _services;
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactionService;

    public CommandsHandler(DiscordSocketClient client)
    {
        _client = client;
        _commandService = new CommandService();
        _interactionService = new InteractionService(_client.Rest);
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commandService)
            .AddSingleton(_interactionService)
            .BuildServiceProvider();
    }

    public async Task InitializeHandlersAsync()
    {
        var commandHandler = new CommandHandler(_client, _commandService, _services);
        var interactionHandler = new InteractionsHandler(_client, _interactionService, _services);

        await commandHandler.InitializeAsync();
        await interactionHandler.InitializeAsync();
    }
    public Task SubscribeDiscordEventsAsync()
    {
        _client.Ready += ClientReady;
        _client.UserJoined += UserJoinedHandler;
        _client.Log += LogHandler;
        
        return Task.CompletedTask;
    }

    private async Task ClientReady()
    {
        await _interactionService.RegisterCommandsGloballyAsync();
    }
    
    private async Task UserJoinedHandler(SocketGuildUser user)
    {
        await user.AddRoleAsync(Convert.ToUInt64(Environment.GetEnvironmentVariable("INITIAL_ROLE")));
    }
    private Task LogHandler(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}