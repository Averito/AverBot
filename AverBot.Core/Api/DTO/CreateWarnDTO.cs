namespace AverBot.Core.DTO;

public class CreateWarnDTO
{
    public string Reason { get; }
    public int ServerId { get; }
    public ulong GuildUserDiscordId { get; }

    public CreateWarnDTO(string reason, int serverId, ulong guildUserDiscordId)
    {
        Reason = reason;
        ServerId = serverId;
        GuildUserDiscordId = guildUserDiscordId;
    }
}