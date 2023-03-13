using Discord.WebSocket;
using PlatformBot.Core.Attributes;

namespace PlatformBot.Core.Commands;

public class GeneralCommands
{
    private DiscordSocketClient _client { get; }
    
    public GeneralCommands(DiscordSocketClient client)
    {
        _client = client;
    }

    [Command("get-me", "Получить себя :D")]
    public Task GetMyName(SocketSlashCommand command)
    {
        command.RespondAsync($"{command.User.Username}#{command.User.Discriminator}");
        return Task.CompletedTask;
    }

    [Command("get-chance", "Не нужное мнение бота :D")]
    public Task GetChance(SocketSlashCommand command)
    {
        var rnd = new Random();
        command.RespondAsync($"{rnd.Next(0, 100)}%");
        return Task.CompletedTask;
    }
}