using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 喜欢歌曲
    /// </summary>
    public static UserCloudApi UserCloudApi = new();
}


public class UserCloudApi : WeApiContractBase<UserCloudRequest, UserCloudResponse, ErrorResultBase, UserCloudActualRequest>
{
    public override string Url => "https://music.163.com/api/v1/cloud/get";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(UserCloudRequest? request)
    {
        ActualRequest = new()
                        {
                            Limit = request?.Limit ?? 30,
                            Offset = request?.Offset ?? 0
                        };
        return Task.CompletedTask;
    }
}

public class UserCloudRequest : RequestBase
{
    /// <summary>
    /// 起始位置
    /// </summary>
    public int Limit { get; set; } = 30;
    
    /// <summary>
    /// 获取数量
    /// </summary>
    public int Offset { get; set; } = 0;
}

public class UserCloudResponse : CodedResponseBase
{
    [JsonPropertyName("hasMore")] public bool HasMore { get; set; }
    [JsonPropertyName("size")] public long MaxSize { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("data")] public UserCloudSongItem[]? Songs { get; set; }
    
    public class UserCloudSongItem
    {
        [JsonPropertyName("album")] public string? AlbumName { get; set; }
        [JsonPropertyName("artist")] public string? ArtistName { get; set; }
        [JsonPropertyName("bitrate")] public int Bitrate { get; set; }
        [JsonPropertyName("songName")] public string? SongName { get; set; }
        [JsonPropertyName("fileName")] public string? FileName { get; set; }
        [JsonPropertyName("songId")] public string? SongId { get; set; }
        [JsonPropertyName("fileSize")] public long FileSize { get; set; }
        [JsonPropertyName("lyricId")] public string? LyricId { get; set; }
        [JsonPropertyName("simpleSong")] public EmittedSongDtoWithPrivilege? SongInfo { get; set; }
    }
}

public class UserCloudActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("limit")] public int Limit { get; set; } = 30;
    [JsonPropertyName("offset")] public int Offset { get; set; }
}