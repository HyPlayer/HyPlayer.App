using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.ApiContracts;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class PlaylistDto
{
    [JsonPropertyName("creator")] public UserInfoDto? Creator { get; set; }
    [JsonPropertyName("privacy")] public int Privacy { get; set; }
    [JsonPropertyName("trackCount")] public int TrackCount { get; set; }
    [JsonPropertyName("coverImgUrl")] public string? CoverUrl { get; set; }
    [JsonPropertyName("updateFrequency")] public string? UpdateFrequency { get; set; }
    [JsonPropertyName("playCount")] public long PlayCount { get; set; }
    [JsonPropertyName("subscribedCount")] public long SubscribedCount { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("updateTime")] public long UpdateTime { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("subscribed")] public bool Subscribed { get; set; }
}