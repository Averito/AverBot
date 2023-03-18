using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AverBot.API.Models;

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
    
    public Server() {}

    public Server(ulong discordId, string name, int userId)
    {
        DiscordId = discordId;
        Name = name;
        UserId = userId;
    }
}