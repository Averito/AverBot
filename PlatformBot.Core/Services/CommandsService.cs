using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace PlatformBot.Core.Services;

public class CommandService
{
    private DiscordSocketClient _client { get; }
    private Dictionary<string[], Func<SocketSlashCommand, Task>> _commands { get; }
    private Dictionary<Predicate<SocketMessage>, Func<SocketMessage, Task>> _messages { get; }

    public CommandService(DiscordSocketClient client)
    {
        _client = client;
        _commands = new Dictionary<string[], Func<SocketSlashCommand, Task>>();
        _messages = new Dictionary<Predicate<SocketMessage>, Func<SocketMessage, Task>>();
    }

    public Task InstallCommandsAsync()
    {
        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;
        _client.MessageReceived += MessageReceivedHandler;
        _client.UserJoined += UserJoinedHandler;
        _client.Log += LogHandler;
        
        return Task.CompletedTask;
    }
    
    
    public void AddCommand(string name, Func<SocketSlashCommand, Task> action, string description = "")
    {
        _commands.Add(new []{name, description}, action);
    }

    public void AddMessage(Predicate<SocketMessage> predicate, Func<SocketMessage, Task> action)
    {
        _messages.Add(predicate, action);
    }

    private async Task UserJoinedHandler(SocketGuildUser user)
    {
        await user.AddRoleAsync(Convert.ToUInt64(Environment.GetEnvironmentVariable("INITIAL_ROLE")));
    }

    private async Task MessageReceivedHandler(SocketMessage message)
    {
        try
        {
            Console.WriteLine("Not Implemented");
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }

    private Task LogHandler(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        try
        {
            foreach (var entries in _commands)
            {
                if (command.Data.Name == entries.Key[0])
                {
                    await entries.Value.Invoke(command);
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