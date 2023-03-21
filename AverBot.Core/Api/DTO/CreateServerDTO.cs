namespace AverBot.Core.DTO;
public class CreateServerDTO
{
    public ulong DiscordId { get; set; }
    public string Name { get; set; }

    public CreateServerDTO(ulong discordId, string name)
    {
        DiscordId = discordId;
        Name = name;
    }
}