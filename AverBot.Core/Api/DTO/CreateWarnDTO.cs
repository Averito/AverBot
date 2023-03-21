namespace AverBot.Core.DTO;

public class CreateWarnDTO
{
    public string Reason { get; set; }
    public int ServerId { get; set; }
    public int GuildUserId { get; set; }

    public CreateWarnDTO(string reason, int serverId, int guildUserId)
    {
        Reason = reason;
        ServerId = serverId;
        GuildUserId = guildUserId;
    }
}