using AverBot.Core.DTO;
using AverBot.Core.Infrastructure.Services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class WarnInteraction : MainInteractionModule
{
    private readonly WarnService _warnService;
    private readonly GuildUserService _guildUserService;
    private readonly ServerService _serverService;

    public WarnInteraction(WarnService warnService, GuildUserService guildUserService, ServerService serverService)
    {
        _warnService = warnService;
        _guildUserService = guildUserService;
        _serverService = serverService;
    }

    [SlashCommand("warn", "Выдать предупреждение плохому человечку")]
    public async Task CreateWarn(SocketUser user, string reason)
    {
        if (CurrentUser == null) return;

        if (!CurrentUser.GuildPermissions.KickMembers)
        {
            await SendErrorMessage("Недостаточно прав, в следующий раз пожалуюсь хозяину -.-");
            return;
        }

        var currentServer = await _serverService.GetByDiscordId(CurrentUser.Guild.Id);
        var currentGuildUser = await _guildUserService.GetByDiscordId(user.Id);
        var warns = await _warnService.GetWarnsByGuildUserIdAndServerId(currentGuildUser.Id, currentServer.Id);

        if (warns.Count == currentServer.Configuration.WarnsLimit)
        {
            await SendErrorMessage("Максимальное количество предупреждений");
            return;
        }

        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return;

        var createWarnDto = new CreateWarnDTO(reason, currentServer.Id, currentGuildUser.Id);
        await _warnService.Create(createWarnDto);

        var successMessage = GetSuccessMessage($"Предупреждение #{warns.Count + 1}.", $"Выдано пользователю: {user.Mention} \nПричина: {reason}", guildUser, Color.Magenta);
        await RespondAsync(embed: successMessage);
    }
}