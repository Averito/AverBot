using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AverBot.Core.Domain.Enums;
using AverBot.Core.DTO;

namespace AverBot.Core.Domain.Entities;

public class ConfigurationPunishment : BaseModel
{
    [Column("type_of_violation")]
    [JsonPropertyName("typeOfViolation")]
    public TypeOfViolation TypeOfViolation { get; set; }
    
    [Column("violation_count")]
    [JsonPropertyName("violationCount")]
    public int ViolationCount { get; set; }

    [Column("punishment_type")]
    [JsonPropertyName("punishmentType")]
    public PunishmentType PunishmentType { get; set; }
    
    [Column("comment")]
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
    
    [Column("time_in_seconds")]
    [JsonPropertyName("timeInSeconds")]
    public int TimeInSeconds { get; set; }
    
    [Column("time_in_minutes")]
    [JsonPropertyName("timeInMinutes")]
    public decimal TimeInMinutes { get; set; }
    
    [Column("time_in_hours")]
    [JsonPropertyName("timeInHours")]
    public decimal TimeInHours { get; set; }
    
    [Column("time_in_days")]
    [JsonPropertyName("timeInDays")]
    public decimal TimeInDays { get; set; }
    
    [Column("configuration")]
    [ForeignKey("ConfigurationId")]
    [JsonPropertyName("configuration")]
    public Configuration Configuration { get; set; }
    
    [Column("configurationId")]
    [JsonPropertyName("configurationId")]
    public int ConfigurationId { get; set; }

    public ConfigurationPunishment(int configurationId, TypeOfViolation typeOfViolation, PunishmentType punishmentType, int violationCount, int timeInSeconds = 3600, string? comment = null) : base()
    {
        ConfigurationId = configurationId;
        TypeOfViolation = typeOfViolation;
        ViolationCount = violationCount;
        PunishmentType = punishmentType;
        TimeInSeconds = timeInSeconds;
        TimeInMinutes = Math.Round((decimal)(timeInSeconds / 60), 1);
        TimeInHours = Math.Round(TimeInMinutes / 60, 1);
        TimeInDays = Math.Round(TimeInHours / 24, 1);
        Comment = comment;
    }
    
    public ConfigurationPunishment(CreatePunishmentDTO createPunishmentDto, int configurationId) : base()
    {
        ConfigurationId = configurationId;
        TypeOfViolation = createPunishmentDto.TypeOfViolation;
        ViolationCount = createPunishmentDto.ViolationCount;
        PunishmentType = createPunishmentDto.PunishmentType;
        TimeInSeconds = createPunishmentDto.TimeInSeconds;
        TimeInMinutes = Math.Round((decimal)(TimeInSeconds / 60), 1);
        TimeInHours = Math.Round(TimeInMinutes / 60, 1);
        TimeInDays = Math.Round(TimeInHours / 24, 1);
        Comment = createPunishmentDto.Comment;
    }
}