using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class CommentDto
{
    [JsonPropertyName("user")] public UserInfoDto? User { get; set; }
    [JsonPropertyName("content")] public string? Content { get; set; }
    [JsonPropertyName("commentId")] public long CommentId { get; set; }
    [JsonPropertyName("likedCount")] public int LikedCount { get; set; }
    [JsonPropertyName("liked")] public bool Liked { get; set; }
    [JsonPropertyName("owner")] public bool Owner { get; set; }
    
}