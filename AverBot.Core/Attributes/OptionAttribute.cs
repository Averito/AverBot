using System.Reflection;
using System.Text.RegularExpressions;
using AverBot.Core.Handlers;
using Discord.WebSocket;
using AverBot.Core.Commands;
using AverBot.Core.Structurs;
using Discord;

namespace AverBot.Core.Attributes;

public class OptionAttribute : Attribute
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
    public ApplicationCommandOptionType Type { get; }
    public string Description { get; }

    public OptionAttribute(string name, ApplicationCommandOptionType type, string description)
    { 
        Name = name;
        Type = type;
        Description = description;
    }

    public static void AddOptions<T>(CommandsHandler commandsHandler, T commands)
    {
        var type = commands.GetType();

        foreach (var methodInfo in type.GetMethods())
        {
            string commandName = "";
            SlashCommandAttribute? commandAttribute = null;
            
            foreach (var customAttribute in methodInfo.GetCustomAttributes())
            {
                if (!(customAttribute is SlashCommandAttribute)) continue;
                
                commandName = (customAttribute as SlashCommandAttribute).Name;
                commandAttribute = (SlashCommandAttribute)customAttribute;
                break;
            }

            foreach (var customAttribute in methodInfo.GetCustomAttributes())
            {
                if (commandAttribute == null) break;
                if (!(customAttribute is OptionAttribute)) continue;
                
                var optionAttribute = (OptionAttribute)customAttribute;
                var option = CreateOption(optionAttribute.Name, optionAttribute.Type, optionAttribute.Description);

                commandsHandler.AddOptionForSlashCommand(commandName, option);
            }
        }
    }

    private static SlashCommandOptionBuilder CreateOption(string name, ApplicationCommandOptionType type, string description)
    {
        var optionBuilder = new SlashCommandOptionBuilder();
        
        optionBuilder.WithName(name);
        optionBuilder.WithType(type);
        optionBuilder.WithDescription(description);

        return optionBuilder;
    }
}