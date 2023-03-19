using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AverBot.API.DTO;

namespace AverBot.API.Models;

public class GuildUser : BaseModel
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

    [Column("warns")]
    [JsonPropertyName("warns")]
    public List<Warn> Warns { get; set; }
    
    [Column("server_guild_users")]
    [JsonPropertyName("serverGuildUsers")]
    public List<ServerGuildUser> ServerGuildUsers { get; set; }

    public GuildUser() {}
    
    public GuildUser(CreateGuildUserDTO createGuildUserDto)
    {
        DiscordId = createGuildUserDto.DiscordId;
        Username = createGuildUserDto.Username;
        Discriminator = createGuildUserDto.Discriminator;
    }
}