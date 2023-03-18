namespace AverBot.API.DTO;

public class LoginResponseDTO
{
    public int Id { get; }
    public ulong DiscordId { get; }
    public string? Token { get; }

    public LoginResponseDTO(int id, ulong discordId, string? token)
    {
        Id = id;
        DiscordId = discordId;
        Token = token;
    }
}