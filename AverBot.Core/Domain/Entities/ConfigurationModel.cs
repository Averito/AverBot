using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AverBot.Core.Domain.Entities;

public class Configuration : BaseModel
{
    [Column("warns_limit")]
    [Required]
    [JsonPropertyName("warnsLimit")]
    public int WarnsLimit { get; set; }
    
    [Column("punishments")]
    [JsonPropertyName("punishments")]
    public List<ConfigurationPunishment> Punishments { get; set; }

    [Column("server")]
    [ForeignKey("ServerId")]
    [JsonPropertyName("server")]
    public Server Server { get; set; }
    
    [Column("server_id")]
    [JsonPropertyName("serverId")]
    public int ServerId { get; set; }
    
    public Configuration() {}

    public Configuration(int serverId, int warnsLimit = 5)
    {
        ServerId = serverId;
        WarnsLimit = warnsLimit;
    }
}