using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class DjProgramDto
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("picUrl")] public string? PictureUrl { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("coverUrl")] public string? CoverUrl { get; set; }
    [JsonPropertyName("duration")] public long Duration { get; set; }
    [JsonPropertyName("dj")] public UserInfoDto? Owner { get; set; }
    [JsonPropertyName("radio")] public DjRadioDto? Radio { get; set; }
}