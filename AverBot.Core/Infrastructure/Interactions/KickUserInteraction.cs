using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class KickUserInteraction : MainInteractionModule
{
    [SlashCommand("kick", "Кикнуть пользователя с сервера")]
    public async Task KickUser(SocketUser user, string reason)
    {
        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return;

        if (CurrentUser != null && !CurrentUser.GuildPermissions.KickMembers)
        {
            var embed = new EmbedBuilder()
                .WithAuthor(CurrentUser.ToString(), CurrentUser.GetAvatarUrl() ?? CurrentUser.GetDefaultAvatarUrl())
                .WithTitle("Недостаточно прав, подумай лучше :D")
                .WithColor(Color.Red)
                .WithCurrentTimestamp()
                .Build();
            await RespondAsync(embed: embed, ephemeral: true);
            return;
        }

        await guildUser.KickAsync(reason);

        var embedBuilder = new EmbedBuilder()
            .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            .WithTitle($"Был кикнут пользователь {guildUser}")
            .WithDescription($"По причине {reason}")
            .WithColor(Color.Red)
            .WithCurrentTimestamp();

        await RespondAsync(embed: embedBuilder.Build());
    }
}