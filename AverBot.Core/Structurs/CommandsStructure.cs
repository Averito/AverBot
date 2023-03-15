using Discord;
using Discord.WebSocket;

namespace AverBot.Core.Structurs;

public struct CommandsStructure
{
    public Func<SocketSlashCommand, Task> Handler { get; }
    public List<SlashCommandOptionBuilder> Options { get; }

    public CommandsStructure(Func<SocketSlashCommand, Task> handler, List<SlashCommandOptionBuilder> options)
    {
        Handler = handler;
        Options = options;
    }
    
    public CommandsStructure(Func<SocketSlashCommand, Task> handler)
    {
        Handler = handler;
        Options = new List<SlashCommandOptionBuilder>();
    }
}
