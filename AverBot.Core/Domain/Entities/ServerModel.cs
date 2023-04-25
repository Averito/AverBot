using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AverBot.Core.DTO;

namespace AverBot.Core.Domain.Entities;

public class Server : BaseModel
{
    [Column("discord_id")]
    [Required]
    [JsonPropertyName("discordId")]
    public ulong DiscordId { get; set; }

    [Column("name")]
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Column("user")]
    [ForeignKey("UserId")]
    [Required]
    [JsonPropertyName("user")]
    public User? User { get; set; }
    
    [Column("user_id")]
    [Required]
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    
    [Column("warns")]
    [JsonPropertyName("warns")]
    public List<Warn> Warns { get; set; }

    [Column("configuration")]
    [JsonPropertyName("configuration")]
    public Configuration Configuration { get; set; }

    public Server() {}

    public Server(CreateServerDTO createServerDto, int userId)
    {
        DiscordId = createServerDto.DiscordId;
        Name = createServerDto.Name;
        UserId = userId;
    }
}