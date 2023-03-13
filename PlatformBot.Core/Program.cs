using Discord;
using Discord.WebSocket;
using PlatformBot.Core.Activities;
using PlatformBot.Core.Attributes;
using PlatformBot.Core.Commands;
using PlatformBot.Core.Services;

// Load environments variables from .env file
var environmentsService = new EnvironmentService(@"D:\Averito\C#\PlatformBot\PlatformBot.Core\.env");
environmentsService.EnvironmentsLoad();

var discord = new DiscordSocketClient(new DiscordSocketConfig()
{
    GatewayIntents = GatewayIntents.All
});
await discord.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
await discord.SetActivityAsync(new OnlineActivity("на Арину"));

var commandService = new CommandService(discord);
await commandService.InstallCommandsAsync();

CommandAttribute.AddCommands(commandService, new GeneralCommands(discord));

await discord.StartAsync();

await Task.Delay(-1);
