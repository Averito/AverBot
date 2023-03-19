using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AverBot.API.Models;

public class ServerGuildUser : BaseModel
{
    [Column("server")]
    [JsonPropertyName("server")]
    public Server Server { get; set; }
    
    [Column("server_id")]
    [JsonPropertyName("serverId")]
    public int ServerId { get; set; }

    [Column("guild_user")]
    [JsonPropertyName("guildUser")]
    public GuildUser GuildUser { get; set; }
    
    [Column("guild_user_id")]
    [JsonPropertyName("guildUserId")]
    public int GuildUserId { get; set; }
    
    public ServerGuildUser() {}

    public ServerGuildUser(int serverId, int guildUserId)
    {
        ServerId = serverId;
        GuildUserId = guildUserId;
    }
}