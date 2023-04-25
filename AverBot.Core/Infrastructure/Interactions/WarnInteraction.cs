using AverBot.Core.Domain.Entities;
using AverBot.Core.Domain.Enums;
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
    private readonly ConfigurationPunishmentService _configurationPunishmentService;

    public WarnInteraction(WarnService warnService, GuildUserService guildUserService, ServerService serverService, ConfigurationPunishmentService configurationPunishmentService)
    {
        _warnService = warnService;
        _guildUserService = guildUserService;
        _serverService = serverService;
        _configurationPunishmentService = configurationPunishmentService;
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
        var warns = await _warnService.GetWarnsByGuildUserDiscordIdAndServerDiscordId(user.Id, CurrentUser.Guild.Id);
        var punishments = await _configurationPunishmentService.GetByTypeOfViolationAndConfigurationId(TypeOfViolation.Warn, currentServer.Configuration.Id);

        if (warns.Count == currentServer.Configuration.WarnsLimit)
        {
            await SendErrorMessage("Максимальное количество предупреждений");
            return;
        }

        var guildUser = (SocketGuildUser?)user;
        if (guildUser == null) return;

        var createWarnDto = new CreateWarnDTO(reason, CurrentUser.Guild.Id, user.Id);
        await _warnService.Create(createWarnDto);

        foreach (var punishment in punishments)
        {
            if (warns.Count + 1 != punishment.ViolationCount) continue;
            
            if (punishment.PunishmentType == PunishmentType.Ban)
            {
                await Context.Guild.AddBanAsync(user.Id, (int)punishment.TimeInDays, punishment.Comment);
                continue;
            }
            
            await guildUser.SetTimeOutAsync(TimeSpan.FromSeconds(punishment.TimeInSeconds));
        }

        var successMessage = GetSuccessMessage($"Предупреждение #{warns.Count + 1}.", $"Выдано пользователю: {user.Mention} \nПричина: {reason}", guildUser, Color.Magenta);
        await RespondAsync(embed: successMessage);
    }
}