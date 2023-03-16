using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Interactions;

public class GetAvatarInteraction : MainInteractionModule
{
    [SlashCommand("avatar", "Получение аватара пользователя")]
    public async Task GetAvatar(SocketUser user)
    {
        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return;

        var embedBuilder = new EmbedBuilder()
            .WithAuthor(user.ToString(),
                user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
            .WithTitle($"Аватар пользователя {guildUser.Username}")
            .WithImageUrl(guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            .WithColor(Color.Green)
            .WithCurrentTimestamp();
    
        await RespondAsync(embed: embedBuilder.Build());
    }
}