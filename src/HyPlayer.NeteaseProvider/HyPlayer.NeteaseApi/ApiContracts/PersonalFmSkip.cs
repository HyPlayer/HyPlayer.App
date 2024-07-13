using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static AiDjSkipApi AiDjSkipApi = new();
}

public class AiDjSkipApi : EApiContractBase<AiDjSkipRequest, AiDjSkipResponse, ErrorResultBase, AiDjSkipActualRequest>
{
    public override string Url => "https://interface3.music.163.com/eapi/v1/radio/skip";
    public override HttpMethod Method => HttpMethod.Post;

    public override async Task<HttpRequestMessage> GenerateRequestMessageAsync<TActualRequestMessageModel>(
        TActualRequestMessageModel actualRequest,
        ApiHandlerOption option,
        CancellationToken cancellationToken = default)
    {
        var res = await base.GenerateRequestMessageAsync(actualRequest, option, cancellationToken).ConfigureAwait(false);
        res.RequestUri =
            new Uri(
                $"https://interface3.music.163.com/eapi/v1/radio/skip?songId={Request?.SongId}&time={Request?.Time ?? 0}&mode={Request?.Mode ?? "DEFAULT"}&subMode={Request?.SubMode}&source=userfm");
        return res;
    }

    public override Task MapRequest(AiDjSkipRequest? request)
    {
        if (request is not null)
            ActualRequest = new AiDjSkipActualRequest
                            {
                                SongId = request.SongId,
                                Time = request.Time
                            };
        return Task.CompletedTask;
    }

    public override string ApiPath => "/api/v1/radio/skip";
}

public class AiDjSkipRequest : RequestBase
{
    public required string SongId { get; set; }
    public long Time { get; set; }

    public string Mode { get; set; } =
        "DEFAULT"; // aidj, DEFAULT, FAMILIAR, EXPLORE, SCENE_RCMD ( EXERCISE, FOCUS, NIGHT_EMO  )

    public string? SubMode { get; set; } = null;
}

public class AiDjSkipResponse : CodedResponseBase
{
}

public class AiDjSkipActualRequest : EApiActualRequestBase
{
    [JsonPropertyName("songId")] public required string SongId { get; set; }
    [JsonPropertyName("time")] public long Time { get; set; }
    [JsonPropertyName("mode")] public string Mode = "DEFAULT";
    [JsonPropertyName("subMode")] public string? SubMode = null;
    [JsonPropertyName("source")] public string Source => "userfm";
}