using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static MusicFirstListenInfoApi MusicFirstListenInfoApi = new();
}

public class MusicFirstListenInfoApi : WeApiContractBase<MusicFirstListenInfoRequest, MusicFirstListenInfoResponse,
    ErrorResultBase, MusicFirstListenInfoActualRequest>
{
    public override string Url => "https://interface3.music.163.com/api/content/activity/music/first/listen/info";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(MusicFirstListenInfoRequest? request)
    {
        if (request is not null)
            ActualRequest = new MusicFirstListenInfoActualRequest()
                            {
                                SongId = request.SongId
                            };
        return Task.CompletedTask;
    }
}

public class MusicFirstListenInfoRequest : RequestBase
{
    public string SongId { get; set; }
}

public class MusicFirstListenInfoResponse : CodedResponseBase
{
    [JsonPropertyName("data")] public MusicFirstListenInfoData? Data { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
    
    public class MusicFirstListenInfoData
    {
        [JsonPropertyName("songInfoDto")] public SongInfoDto? SongInfo { get; set; }
        [JsonPropertyName("musicFirstListenDto")] public MusicFirstListenDto? MusicFirstListen { get; set; }
        [JsonPropertyName("musicTotalPlayDto")] public MusicTotalPlayDto? MusicTotalPlay { get; set; }
        [JsonPropertyName("musicPlayMostDto")] public MusicPlayMostDto? MusicPlayMost { get; set; }
        [JsonPropertyName("musicLikeSongDto")] public MusicLikeSongDto? MusicLikeSong { get; set; }

        public class SongInfoDto
        {
            [JsonPropertyName("songId")] public string? SongId { get; set; }
            [JsonPropertyName("songName")] public string? SongName { get; set; }
            [JsonPropertyName("singer")] public string? Singer { get; set; }
            [JsonPropertyName("coverUrl")] public string? CoverUrl { get; set; }
            [JsonPropertyName("type")] public int Type { get; set; }
        }

        public class MusicFirstListenDto
        {
            [JsonPropertyName("date")] public string? Date { get; set; }
            [JsonPropertyName("season")] public string? Season { get; set; }
            [JsonPropertyName("period")] public string? Period { get; set; }
            [JsonPropertyName("time")] public string? Time { get; set; }
        }

        public class MusicTotalPlayDto
        {
            [JsonPropertyName("playCount")] public int PlayCount { get; set; }
            [JsonPropertyName("duration")] public int Duration { get; set; }
            [JsonPropertyName("text")] public string? Text { get; set; }
        }

        public class MusicPlayMostDto
        {
            [JsonPropertyName("date")] public string? Date { get; set; }
            [JsonPropertyName("text")] public string? Text { get; set; }
        }

        public class MusicLikeSongDto
        {
            [JsonPropertyName("text")] public string? Text { get; set; }
            [JsonPropertyName("like")] public bool Like { get; set; }
            [JsonPropertyName("collect")] public bool Collect { get; set; }
        }
        
    }
}

public class MusicFirstListenInfoActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("songId")] public required string SongId { get; set; }
}