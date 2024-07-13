using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class PrivilegeDto
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("plLevel")] public string? PlayLevel { get; set; }
}