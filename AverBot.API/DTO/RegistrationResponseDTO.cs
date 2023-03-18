using AverBot.API.Models;

namespace AverBot.API.DTO;

public class RegistrationResponseDTO
{
    public User User { get; }
    public string? Token { get; }

    public RegistrationResponseDTO(User user, string? token)
    {
        User = user;
        Token = token;
    }
}