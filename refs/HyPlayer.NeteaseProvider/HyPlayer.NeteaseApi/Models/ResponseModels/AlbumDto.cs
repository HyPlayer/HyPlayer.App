using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class AlbumDto
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("picUrl")] public string? PictureUrl { get; set; }
}