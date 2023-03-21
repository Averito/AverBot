namespace AverBot.Core.DTO;

public class CreateGuildUserDTO
{
    public ulong DiscordId { get; set; }
    public string Username { get; set; }
    public int Discriminator { get; set; }

    public CreateGuildUserDTO(ulong discordId, string username, int discriminator)
    {
        DiscordId = discordId;
        Username = username;
        Discriminator = discriminator;
    }
}