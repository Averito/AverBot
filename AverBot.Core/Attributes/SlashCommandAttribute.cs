using System.Reflection;
using System.Text.RegularExpressions;
using AverBot.Core.Handlers;
using Discord.WebSocket;
using AverBot.Core.Commands;
using AverBot.Core.Structurs;

namespace AverBot.Core.Attributes;

public class SlashCommandAttribute: Attribute
{
    private string _name { get; set; }
    public string Name
    {
        get => _name;
        set
        {
            var regex = new Regex(@"^[\w-]{3,32}$");
            if (regex.IsMatch(value)) _name = value;
        }
    }

    public string Description { get; }

    public SlashCommandAttribute(string name)
    {
        Name = name;
        Description = "";
    }

    public SlashCommandAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static void AddCommands<T>(CommandsHandler commandsHandler, T commands)
    {
        var type = commands.GetType();

        foreach (var methodInfo in type.GetMethods())
        {
            foreach (var customAttribute in methodInfo.GetCustomAttributes())
            {
                if (!(customAttribute is SlashCommandAttribute)) continue;

                var commandAttribute = (SlashCommandAttribute)customAttribute;
                
                var action = (Func<SocketSlashCommand, Task>)Delegate.CreateDelegate(typeof(Func<SocketSlashCommand, Task>), commands, methodInfo);
                var commandsStructure = new CommandsStructure(action);
                
                commandsHandler.AddSlashCommand(commandAttribute.Name, commandsStructure, commandAttribute.Description);
            }
        }
    }
}