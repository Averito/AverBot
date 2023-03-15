using AverBot.Core.Attributes;
using AverBot.Core.Commands;
using AverBot.Core.Handlers;
using Discord;
using Discord.WebSocket;

namespace AverBot.Core.Services;

public class StartupService
{
    private DiscordSocketClient _client { get; }
    private DiscordSocketConfig _config { get; } = new ()
    {
        GatewayIntents = GatewayIntents.All
    };
    private CommandsHandler _commandsHandler { get; }
    private GeneralCommands? _generalCommands { get; set; }
    
    public StartupService()
    {
        _client = new DiscordSocketClient(_config);
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
    public async Task SubscribeDiscordEventsAsync()
    {
        await _commandsHandler.SubscribeDiscordEventsAsync();
    }
    public Task InstallCommandsAsync()
    {
        _generalCommands = new GeneralCommands(_client);
        
        return Task.CompletedTask;
    }
    public Task InstallAttributesAsync()
    {
        SlashCommandAttribute.AddCommands(_commandsHandler, _generalCommands);
        CommandAttribute.AddCommands(_commandsHandler, _generalCommands);
        OptionAttribute.AddOptions(_commandsHandler, _generalCommands);

        return Task.CompletedTask;
    }
}