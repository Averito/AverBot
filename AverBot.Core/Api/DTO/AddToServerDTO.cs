namespace AverBot.Core.DTO;

public class AddToServerDTO
{
    public ulong GuildUserDiscordId { get; set; }

    public AddToServerDTO(ulong guildUserDiscordId)
    {
        GuildUserDiscordId = guildUserDiscordId;
    }
}