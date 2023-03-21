namespace AverBot.Core.DTO;

public class LoginDTO
{
    public int Id { get; }
    public DateTime ExpiresIn { get; }

    public LoginDTO(int id, DateTime expiresIn)
    {
        Id = id;
        ExpiresIn = expiresIn;
    }
}