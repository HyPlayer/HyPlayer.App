using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class RecommendPlaylistDto
{
    [JsonPropertyName("creator")] public UserInfoDto? Creator { get; set; }
    [JsonPropertyName("trackCount")] public int TrackCount { get; set; }
    [JsonPropertyName("picUrl")] public string? CoverUrl { get; set; }
    [JsonPropertyName("playCount")] public long PlayCount { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
}