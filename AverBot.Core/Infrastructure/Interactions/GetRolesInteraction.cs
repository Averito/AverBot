using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class GetRoleInteraction : MainInteractionModule
{
    [SlashCommand("list-roles", "Показать все роли пользователя")]
    public Task GetRolesList(SocketUser user)
    {
        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return RespondAsync("Что-то пошло не так...");
        
        var roleList = string.Join(",\n", guildUser.Roles.Where(role => !role.IsEveryone).Select(role => role.Mention));
        
        var embedBuilder = new EmbedBuilder()
            .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            .WithTitle($"Роли пользователя {guildUser}")
            .WithDescription(roleList)
            .WithColor(Color.Gold)
            .WithCurrentTimestamp();
        
        RespondAsync(embed: embedBuilder.Build());
        return Task.CompletedTask;
    }
}