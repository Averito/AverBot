using AverBot.Core.Infrastructure.Handlers;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Services;

public class BotStartupService
{
    private readonly DiscordSocketClient _client;
    public DiscordSocketClient Client
    {
        get => _client;
    }
    private readonly CommandsHandler _commandsHandler;

    public BotStartupService(IServiceCollection serviceCollection)
    {
        _client = new DiscordSocketClient(new ()
        {
            GatewayIntents = GatewayIntents.All,
            UseInteractionSnowflakeDate = true
        });
        
        _commandsHandler = new CommandsHandler(_client, serviceCollection);
    }

    public async Task LoginDiscordAsync()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
    }
    public async Task SetDiscordActivityAsync(IActivity activity)
    {
        await _client.SetActivityAsync(activity);
    }
    public async Task StartDiscordAsync()
    {
        await _client.StartAsync();
    }
    public async Task RegisterCommandsAsync()
    {
        await _commandsHandler.InitializeHandlersAsync();
    }
    public async Task SubscribeDiscordEventsAsync()
    {
        await _commandsHandler.SubscribeDiscordEventsAsync();
    }
}