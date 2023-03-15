using AverBot.Core.Structurs;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace AverBot.Core.Handlers;

public class CommandsHandler
{
    private DiscordSocketClient _client { get; }
    private Dictionary<string[], CommandsStructure> _commands { get; }
    private Dictionary<string, Func<SocketMessage, Task>> _messages { get; }

    public CommandsHandler(DiscordSocketClient client)
    {
        _client = client;
        _commands = new Dictionary<string[], CommandsStructure>();
        _messages = new Dictionary<string, Func<SocketMessage, Task>>();
    }

    public Task SubscribeDiscordEventsAsync()
    {
        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;
        _client.MessageReceived += MessageReceivedHandler;
        _client.UserJoined += UserJoinedHandler;
        _client.Log += LogHandler;
        
        return Task.CompletedTask;
    }
    
    
    public void AddSlashCommand(string name, CommandsStructure action, string description = "")
    {
        _commands.Add(new []{name, description}, action);
    }

    public void AddOptionForSlashCommand(string name, SlashCommandOptionBuilder option)
    {
        var command = _commands.First(command => command.Key[0] == name);
        
        command.Value.Options.Add(option);
    }

    public void AddCommand(string name, Func<SocketMessage, Task> handler)
    {
        _messages.Add(name, handler);
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
                
                if (entries.Value.Options.Count != 0) command.AddOptions(entries.Value.Options.ToArray());

                await _client.CreateGlobalApplicationCommandAsync(command.Build());
            }
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        try
        {
            foreach (var entries in _commands)
            {
                if (command.Data.Name != entries.Key[0]) continue;
                await entries.Value.Handler.Invoke(command);
            }
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
    private async Task MessageReceivedHandler(SocketMessage message)
    {
        try
        {
            if (string.IsNullOrEmpty(message.Content)) return;
            if (!message.Content.StartsWith("!")) return;

            var commandName = message.Content.Split(" ")[0];
            foreach (var entries in _messages)
            {
                if (commandName != $"!{entries.Key}") continue;
                await entries.Value.Invoke(message);
            }
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
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