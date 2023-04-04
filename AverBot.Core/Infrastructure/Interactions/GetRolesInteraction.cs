using AverBot.Core.Infrastructure.Services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class GetRoleInteraction : MainInteractionModule
{
    [SlashCommand("list-roles", "Показать все роли пользователя")]
    public async Task GetRolesList(SocketUser user)
    {
        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null)
        {
            await SendErrorMessage("Что-то пошло не так...");
            return;
        }
        
        var roleList = string.Join(",\n", guildUser.Roles.Where(role => !role.IsEveryone).Select(role => role.Mention));

        var embed = GetSuccessMessage($"Роли пользователя {guildUser}", roleList, guildUser, Color.Gold);

        await RespondAsync(embed: embed);
    }
}