using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class UserInfoDto
{
    [JsonPropertyName("userId")] public string? UserId { get; set; }
    [JsonPropertyName("vipType")] public int VipType { get; set; }
    [JsonPropertyName("nickname")] public string? Nickname { get; set; }
    [JsonPropertyName("birthday")] public long? Birthday { get; set; }
    [JsonPropertyName("gender")] public int? Gender { get; set; }
    [JsonPropertyName("avatarUrl")] public string? AvatarUrl { get; set; }
    [JsonPropertyName("backgroundUrl")] public string? BackgroundUrl { get; set; }
    [JsonPropertyName("signature")] public string? Signature { get; set; }
    [JsonPropertyName("followed")] public bool? Followed { get; set; }
}