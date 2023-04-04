using System.Net;
using Discord;
using Discord.Interactions;

namespace AverBot.Core.Infrastructure.Interactions;

public class AddEmojiInteraction : MainInteractionModule
{
    [SlashCommand("emoji", "Добавить эмоцию на сервер")]
    public async Task AddEmoji(string emote, string name)
    {
        if (CurrentUser == null) return;

        Emote resultEmote;
        var canParse = GuildEmote.TryParse(emote, out resultEmote);

        if (!canParse)
        {
            await SendErrorMessage("Эмоция отправлена в не верном формате");
            return;
        }
        
        if (!CurrentUser.GuildPermissions.Administrator)
        {
            await SendErrorMessage("Недостаточно прав :D");
            return;
        }
        
        var stream = await new HttpClient().GetStreamAsync(resultEmote.Url);
        var emoteImage = new Image(stream);
        
        var createdEmote = await CurrentUser.Guild.CreateEmoteAsync(name, emoteImage);

        var responseMessage = GetSuccessMessage($"Эмоция {createdEmote} успешно добавлена на сервер!");
        await RespondAsync(embed: responseMessage);
    }
}