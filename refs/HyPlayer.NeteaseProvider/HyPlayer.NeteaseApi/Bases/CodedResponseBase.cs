using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Bases;

public class CodedResponseBase : ResponseBase
{
    [JsonPropertyName("code")] public int Code { get; set; }
}