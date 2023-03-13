using System.Reflection;
using System.Text.RegularExpressions;
using Discord.WebSocket;
using PlatformBot.Core.Commands;
using PlatformBot.Core.Services;

namespace PlatformBot.Core.Attributes;

public class CommandAttribute: Attribute
{
    private string _name { get; }
    public string Name
    {
        get => _name;
    }

    private string? _description { get; }
    public string? Description
    {
        get => _description;
    }

    public CommandAttribute(string name)
    {
        _name = name;
    }

    public CommandAttribute(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public static void AddCommands<T>(CommandService commandService, T commands)
    {
        var type = commands.GetType();

        foreach (var methodInfo in type.GetMethods())
        {
            foreach (var customAttribute in methodInfo.GetCustomAttributes())
            {
                if (!(customAttribute is CommandAttribute)) continue;

                var commandAttribute = (CommandAttribute)customAttribute;
                var action = (Func<SocketSlashCommand, Task>)Delegate.CreateDelegate(typeof(Func<SocketSlashCommand, Task>), commands, methodInfo);
                
                commandService.AddCommand(commandAttribute.Name, action, commandAttribute.Description);
            }
        }
    }
}