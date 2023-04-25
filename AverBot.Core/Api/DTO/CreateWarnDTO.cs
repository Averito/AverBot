namespace AverBot.Core.DTO;

public class CreateWarnDTO
{
    public string Reason { get; }
    public ulong ServerDiscordId { get; }
    public ulong GuildUserDiscordId { get; }

    public CreateWarnDTO(string reason, ulong serverDiscordId, ulong guildUserDiscordId)
    {
        Reason = reason;
        ServerDiscordId = serverDiscordId;
        GuildUserDiscordId = guildUserDiscordId;
    }
}