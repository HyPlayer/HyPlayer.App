using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class SongWithPrivilegeDto : SongDto
{
    [JsonPropertyName("privilege")] public PrivilegeDto? Privilege { get; set; }
}