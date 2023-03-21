namespace AverBot.Core.DTO;

public class RegistrationDTO
{
    public ulong DiscordId { get; }
    public string Username { get; }
    public int Discriminator { get; }
    public DateTime ExpiresIn { get; }
    public string? Avatar { get; }
    

    public RegistrationDTO(ulong discordId, string username, int discriminator, DateTime expiresIn, string? avatar)
    {
        DiscordId = discordId;
        Username = username;
        Discriminator = discriminator;
        ExpiresIn = expiresIn;
        Avatar = avatar;
    }
}