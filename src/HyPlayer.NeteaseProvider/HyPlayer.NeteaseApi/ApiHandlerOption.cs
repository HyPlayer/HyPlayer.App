using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Extensions.JsonSerializer;

namespace HyPlayer.NeteaseApi;

public class ApiHandlerOption
{
    public Dictionary<string, string> Cookies { get; } = new();

    // ReSharper disable once InconsistentNaming
    public string? XRealIP { get; set; } = null;
    public bool UseProxy { get; set; } = false;
    public IWebProxy? Proxy { get; set; } = null;
    public string? UserAgent { get; set; } = null;
    public bool DegradeHttp { get; set; } = false;

    public JsonSerializerOptions JsonSerializerOptions =
        new(JsonSerializerOptions.Default)
        {
            NumberHandling = JsonNumberHandling.WriteAsString |
                             JsonNumberHandling.AllowReadingFromString,
            AllowTrailingCommas = true,
            Converters = { new NumberToStringConverter(), new JsonBooleanConverter() }
        };
}