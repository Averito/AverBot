using AverBot.Core.Activities;
using AverBot.Core.Handlers;
using AverBot.Core.Services;

var environmentsService = new EnvironmentService(@"D:\Averito\C#\AverBot\AverBot.Core\.env");
environmentsService.EnvironmentsLoad();

var startupService = new StartupService();

await startupService.LoginDiscordAsync();
await startupService.SetDiscordActivityAsync(new WatchingActivity("на Арину"));
await startupService.SubscribeDiscordEventsAsync();
await startupService.InstallCommandsAsync();
await startupService.InstallAttributesAsync();
await startupService.StartDiscordAsync();

await Task.Delay(-1);
