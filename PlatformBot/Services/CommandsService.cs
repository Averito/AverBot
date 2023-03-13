using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace PlatformBot.Services;

public class CommandService
{
    private DiscordSocketClient _client { get; }
    private Dictionary<string[], Action<SocketSlashCommand>> _commands { get; }

    public CommandService(DiscordSocketClient client)
    {
        _client = client;
        _commands = new Dictionary<string[], Action<SocketSlashCommand>>();
    }

    public async Task InstallCommandsAsync()
    {
        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;
    }

    public void AddCommand(string name, Action<SocketSlashCommand> action, string? description)
    {
        _commands.Add(new []{name, description ?? ""}, action);
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        try
        {
            foreach (var entries in _commands)
            {
                if (command.Data.Name == entries.Key[0])
                {
                    entries.Value.Invoke(command);
                }
            }
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
       
    }

    private async Task ClientReady()
    {

        try
        {
            foreach (var entries in _commands)
            {
                var command = new SlashCommandBuilder();
                
                command.WithName(entries.Key[0]);
                command.WithDescription(entries.Key[1]);
                
                await _client.CreateGlobalApplicationCommandAsync(command.Build());
            }
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}