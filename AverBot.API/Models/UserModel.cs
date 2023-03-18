using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AverBot.API.DTO;

namespace AverBot.API.Models;

public class User : BaseModel
{
    [Column("discord_id")]
    [Required]
    [JsonPropertyName("discordId")]
    public ulong DiscordId { get; set; }

    [Column("username")]
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [Column("discriminator")]
    [Required]
    [JsonPropertyName("discriminator")]
    public int Discriminator { get; set; }
    
    [Column("avatar")]
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    
    [Column("servers")]
    [Required]
    [JsonPropertyName("servers")]
    public List<Server> Servers { get; set; }

    public User() {}
    public User(RegistrationDTO registrationDto)
    {
        DiscordId = registrationDto.DiscordId;
        Username = registrationDto.Username;
        Discriminator = registrationDto.Discriminator;
        Avatar = registrationDto.Avatar;
    }

    public void EditUsername(string? username)
    {
        if (string.IsNullOrEmpty(username)) return;
        Username = username;
    }
    public void EditDiscriminator(int? discriminator)
    {
        if (discriminator == null) return;
        Discriminator = (int)discriminator;
    }
    public void EditAvatar(string? avatar)
    {
        if (string.IsNullOrEmpty(avatar)) return;
        Avatar = avatar;
    }
}