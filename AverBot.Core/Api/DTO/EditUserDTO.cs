namespace AverBot.Core.DTO;

public class EditUserDTO
{
    public string? Username { get; set; }
    public int? Discriminator { get; set; }
    public string? Avatar { get; set; }

    public EditUserDTO(string? username, int? discriminator, string? avatar)
    {
        Username = username;
        Discriminator = discriminator;
        Avatar = avatar;
    }
}