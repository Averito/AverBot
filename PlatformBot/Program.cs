using Discord;
using Discord.WebSocket;
using PlatformBot.Services;

// Load environments variables from .env file
var environmentsService = new EnvironmentService(@"D:\Averito\C#\PlatformBot\PlatformBot\.env");
environmentsService.EnvironmentsLoad();

var discord = new DiscordSocketClient();
await discord.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));

var commandHandler = new CommandService(discord);
await commandHandler.InstallCommandsAsync();

await discord.StartAsync();

await Task.Delay(-1);
