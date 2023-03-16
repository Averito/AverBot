using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AverBot.Core.Handlers;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _command;
    private readonly ServiceProvider _services;
    
    public CommandHandler(DiscordSocketClient client, CommandService command, ServiceProvider services)
    {
        _client = client;
        _command = command;
        _services = services;
    }

    public async Task InitializeAsync()
    {
        await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        _client.MessageReceived += MessageReceivedHandler;
    }
    
    private async Task MessageReceivedHandler(SocketMessage message)
    {
        try
        {
            var msg = message as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;

            if (!msg.HasCharPrefix('!', ref argPos) || msg.Author.IsBot) return;

            var context = new SocketCommandContext(_client, msg);
            await _command.ExecuteAsync(context, argPos, _services);
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}