using System.Reflection;
using System.Text.RegularExpressions;
using AverBot.Core.Handlers;
using Discord.WebSocket;
using AverBot.Core.Commands;
using AverBot.Core.Structurs;

namespace AverBot.Core.Attributes;

public class CommandAttribute: Attribute
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

    public CommandAttribute(string name)
    {
        Name = name;
    }

    public static void AddCommands<T>(CommandsHandler commandsHandler, T commands)
    {
        var type = commands.GetType();

        foreach (var methodInfo in type.GetMethods())
        {
            foreach (var customAttribute in methodInfo.GetCustomAttributes())
            {
                if (!(customAttribute is CommandAttribute)) continue;

                var commandAttribute = (CommandAttribute)customAttribute;
                
                var action = (Func<SocketMessage, Task>)Delegate.CreateDelegate(typeof(Func<SocketMessage, Task>), commands, methodInfo);

                commandsHandler.AddCommand(commandAttribute.Name, action);
            }
        }
    }
}