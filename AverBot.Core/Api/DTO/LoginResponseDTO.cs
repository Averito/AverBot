namespace AverBot.Core.DTO;

public class LoginResponseDTO
{
    public int Id { get; }
    public ulong DiscordId { get; }
    public int CurrentServerId { get; }
    public string? Token { get; }

    public LoginResponseDTO(int id, ulong discordId, int currentServerId, string? token)
    {
        Id = id;
        DiscordId = discordId;
        CurrentServerId = currentServerId;
        Token = token;
    }
}