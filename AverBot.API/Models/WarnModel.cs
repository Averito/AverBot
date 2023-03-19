﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AverBot.API.Models;

public class Warn : BaseModel
{
    [Column("reason")]
    [Required]
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
    
    [Column("guild_user")]
    [ForeignKey("GuildUserId")]
    [JsonPropertyName("guildUser")]
    public GuildUser? GuildUser { get; set; }
    
    [Column("guild_user_id")]
    [JsonPropertyName("guildUserId")]
    public int GuildUserId { get; set; }
    
    [Column("server")]
    [ForeignKey("ServerId")]
    [JsonPropertyName("server")]
    public Server? Server { get; set; }
    
    [Column("server_id")]
    [JsonPropertyName("serverId")]
    public int ServerId { get; set; }
    
    public Warn() {}

    public Warn(string reason, int serverId, int guildUserId)
    {
        Reason = reason;
        ServerId = serverId;
        GuildUserId = guildUserId;
    }
}

