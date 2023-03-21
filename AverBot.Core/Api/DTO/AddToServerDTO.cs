namespace AverBot.Core.DTO;

public class AddToServerDTO
{
    public ulong GuildUserDiscordId { get; set; }
    public int ServerId { get; set; }

    public AddToServerDTO(ulong guildUserDiscordId, int serverId)
    {
        GuildUserDiscordId = guildUserDiscordId;
        ServerId = serverId;
    }
}