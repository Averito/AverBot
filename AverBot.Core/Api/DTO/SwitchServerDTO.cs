namespace AverBot.Core.DTO;

public class SwitchServerDTO
{
    public int ServerId { get; }
    public DateTime ExpiresIn { get; }

    public SwitchServerDTO(int serverId, DateTime expiresIn)
    {
        ServerId = serverId;
        ExpiresIn = expiresIn;
    }
}