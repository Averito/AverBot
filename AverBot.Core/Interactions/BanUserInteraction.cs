using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Interactions;

public class BanUserInteraction : MainInteractionModule
{
    [SlashCommand("ban", "Забанить пользователя")]
    public async Task BanUser(SocketUser user, int days, string reason)
    {
        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return;

        if (CurrentUser != null && !CurrentUser.GuildPermissions.BanMembers)
        {
            var embed = new EmbedBuilder()
                .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
                .WithTitle("Недостаточно прав, подумай лучше :D")
                .WithColor(Color.Red)
                .WithCurrentTimestamp()
                .Build();
            await RespondAsync(embed: embed, ephemeral: true);
            return;
        }

        await guildUser.BanAsync(pruneDays: days, reason: reason);

        var embedBuilder = new EmbedBuilder()
            .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            .WithTitle($"Был забанен пользователь {guildUser}")
            .WithDescription($"По причине {reason}")
            .WithColor(Color.Red)
            .WithCurrentTimestamp();

        await RespondAsync(embed: embedBuilder.Build());
    }
}