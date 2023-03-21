using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class VoiceChannelTimeInteraction : MainInteractionModule
{
    [SlashCommand("voice-timer", "Поставить таймер на голосовой канал :D")]
    public async Task SetVoiceChannelTimer(SocketVoiceChannel voiceChannel, ushort minutes, string channelEvent)
    {
        if (minutes < 1 || minutes > 60)
        {
            await SendErrorMessage("Колво минут должно быть в диапазоне от 1 до 60");
            return;
        }

        var startEmbed = GetSuccessMessage($"Событие \"{channelEvent}\"", $"Событие \"{channelEvent}\" начнётся через {minutes} минут", CurrentUser, Color.Magenta);
        
        await RespondAsync(embed: startEmbed);

        for (var i = minutes; i > 0; i--)
        {
            await Task.Delay(60000);
            
            if (i - 1 <= 0) break;
            await SendNotificationMessage($"{i - 1} минут осталось до события \"{channelEvent}\"");
        }

        var users = voiceChannel.ConnectedUsers.ToList();
        var usersDescription = string.Join(",", users.Select(user => user.Mention));

        var endEmbed = GetSuccessMessage($"Событие {channelEvent} началось");

        await Context.Channel.SendMessageAsync(embed: endEmbed);
        await Context.Channel.SendMessageAsync($"РОТА ПОДЪЁМ {usersDescription}!!");
    }
}