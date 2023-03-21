using AverBot.Core.Domain.Entities;

namespace AverBot.Core.DTO;

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