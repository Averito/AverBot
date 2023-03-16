using AverBot.Core.Handlers;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AverBot.Core.Services;

public class StartupService
{
    private readonly IConfigurationRoot _config;
    private readonly DiscordSocketClient _client;
    private readonly CommandsHandler _commandsHandler;
    
    public StartupService()
    {
        _client = new DiscordSocketClient(new ()
        {
            GatewayIntents = GatewayIntents.All,
            UseInteractionSnowflakeDate = true
        });

        _config = new ConfigurationBuilder()
            .AddJsonFile($"{AppContext.BaseDirectory}/../../../appsettings.json")
            .Build();
        _commandsHandler = new CommandsHandler(_client);
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