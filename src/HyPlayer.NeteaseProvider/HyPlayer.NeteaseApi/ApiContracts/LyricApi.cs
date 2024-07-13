using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 歌词
    /// </summary>
    public static LyricApi LyricApi = new();
}

public class LyricApi : EApiContractBase<LyricRequest, LyricResponse, ErrorResultBase, LyricActualRequest>
{
    public override string Url => "https://interface3.music.163.com/eapi/song/lyric/v1";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(LyricRequest? request)
    {
        if (request is not null)
            ActualRequest = new()
                            {
                                Id = request.Id
                            };
        return Task.CompletedTask;
    }

    public override string ApiPath => "/api/song/lyric/v1";
}

public class LyricActualRequest : EApiActualRequestBase
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("cp")] public bool Cp => false;
    [JsonPropertyName("tv")] public int Tv => 0;
    [JsonPropertyName("lv")] public int Lv => 0;
    [JsonPropertyName("rv")] public int Rv => 0;
    [JsonPropertyName("kv")] public int Kv => 0;
    [JsonPropertyName("yv")] public int Yv => 0;
    [JsonPropertyName("ytv")] public int Ytv => 0;
    [JsonPropertyName("yrv")] public int Yrv => 0;
}

public class LyricRequest : RequestBase
{
    /// <summary>
    /// 歌曲 ID
    /// </summary>
    public required string Id { get; set; }
}

public class LyricResponse : CodedResponseBase
{
    [JsonPropertyName("transUser")] public LyricUserInfo? TranslationUser { get; set; }
    [JsonPropertyName("lyricUser")] public LyricUserInfo? LyricUser { get; set; }
    [JsonPropertyName("lrc")] public LyricInfo? Lyric { get; set; }
    [JsonPropertyName("klyric")] public LyricInfo? OldKaraokLyric { get; set; }
    [JsonPropertyName("tlyric")] public LyricInfo? TranslationLyric { get; set; }
    [JsonPropertyName("romalrc")] public LyricInfo? RomajiLyric { get; set; }

    [JsonPropertyName("yrc")] public LyricInfo? YunLyric { get; set; }
    [JsonPropertyName("ytlrc")] public LyricInfo? YunTranslationLyric { get; set; }
    [JsonPropertyName("yromalrc")] public LyricInfo? YunRomajiLyric { get; set; }


    public class LyricInfo
    {
        [JsonPropertyName("version")] public int Version { get; set; }
        [JsonPropertyName("lyric")] public string? Lyric { get; set; }
    }

    public class LyricUserInfo
    {
        [JsonPropertyName("id")] public string? LyricId { get; set; }
        [JsonPropertyName("userid")] public string? UserId { get; set; }
        [JsonPropertyName("nickname")] public string? Nickname { get; set; }
        [JsonPropertyName("uptime")] public long UpdateTime { get; set; }
    }
}