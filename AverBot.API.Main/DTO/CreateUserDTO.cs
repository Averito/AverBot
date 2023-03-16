namespace AverBot.API.Main.DTO;

public class CreateUserDTO
{
    public ulong DiscordId { get; }
    public string Username { get; }
    public int Discriminator { get; }
    public string? Avatar { get; }

    public CreateUserDTO(ulong discordId, string username, int discriminator, string? avatar)
    {
        DiscordId = discordId;
        Username = username;
        Discriminator = discriminator;
        Avatar = avatar;
    }
}