namespace AverBot.API.DTO;

public class CreateGuildUserDTO
{
    public ulong DiscordId { get; set; }
    public string Username { get; set; }
    public int Discriminator { get; set; }
    public int WarnsLimit { get; set; }

    public CreateGuildUserDTO(ulong discordId, string username, int discriminator, int warnsLimit = 5)
    {
        DiscordId = discordId;
        Username = username;
        Discriminator = discriminator;
        WarnsLimit = warnsLimit;
    }
}