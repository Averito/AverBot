namespace AverBot.Core.DTO;

public class LoginDTO
{
    public ulong DiscordId { get; }
    public DateTime ExpiresIn { get; }

    public LoginDTO(ulong discordId, DateTime expiresIn)
    {
        DiscordId = discordId;
        ExpiresIn = expiresIn;
    }
}