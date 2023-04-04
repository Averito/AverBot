namespace AverBot.Core.DTO;
public class CreateServerDTO
{
    public ulong DiscordId { get; }
    public string Name { get; }

    public CreateServerDTO(ulong discordId, string name)
    {
        DiscordId = discordId;
        Name = name;
    }
}