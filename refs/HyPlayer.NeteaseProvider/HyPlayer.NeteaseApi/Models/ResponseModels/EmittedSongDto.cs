using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class EmittedSongDto
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("dt")] public long Duration { get; set; }
    [JsonPropertyName("alia")] public string[]? Alias { get; set; }
    [JsonPropertyName("tns")] public string[]? Translations { get; set; }
    [JsonPropertyName("mv")] public string? MvId { get; set; }
    [JsonPropertyName("cd")] public string? CdName { get; set; }
    [JsonPropertyName("no")] public int TrackNumber { get; set; }
    [JsonPropertyName("al")] public AlbumDto? Album { get; set; }
    [JsonPropertyName("ar")] public ArtistDto[]? Artists { get; set; }
}