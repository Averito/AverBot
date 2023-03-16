using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AverBot.API.Main.DTO;

namespace AverBot.API.Main.Models;

public class User
{
    [Column("user_id")]
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

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

    public User(int id, ulong discordId, string username, int discriminator,
        string? avatar = null)
    {
        Id = id;
        DiscordId = discordId;
        Username = username;
        Discriminator = discriminator;
        Avatar = avatar;
    }
    public User(CreateUserDTO createUserDto)
    {
        DiscordId = createUserDto.DiscordId;
        Username = createUserDto.Username;
        Discriminator = createUserDto.Discriminator;
        Avatar = createUserDto.Avatar;
    }
}