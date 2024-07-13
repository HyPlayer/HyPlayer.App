using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class DjRadioDto
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("subed")] public bool Subscribed { get; set; }
    [JsonPropertyName("programCount")] public int ProgramCount { get; set; }
    [JsonPropertyName("desc")] public string? Description { get; set; }
    [JsonPropertyName("subCount")] public long SubscribedCount { get; set; }
    [JsonPropertyName("picUrl")] public string? CoverUrl { get; set; }
    [JsonPropertyName("category")] public string? Category { get; set; }
    [JsonPropertyName("secondCategory")] public string? SecondCategory { get; set; }
    [JsonPropertyName("likedCount")] public long LikedCount { get; set; }
    [JsonPropertyName("commentCount")] public long CommentCount { get; set; }
}