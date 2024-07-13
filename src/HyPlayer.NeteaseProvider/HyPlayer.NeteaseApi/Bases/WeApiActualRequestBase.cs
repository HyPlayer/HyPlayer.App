using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Bases;

public class WeApiActualRequestBase : ActualRequestBase
{
    [JsonPropertyName("csrf_token")]
    public string? CsrfToken { get; set; }
}