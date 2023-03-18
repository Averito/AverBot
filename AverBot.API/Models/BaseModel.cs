using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AverBot.API.Models;

public class BaseModel
{
    [Column("id")]
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }
}