using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 歌手歌曲
    /// </summary>
    public static ArtistSongsApi ArtistSongsApi = new();
}

public class ArtistSongsApi : WeApiContractBase<ArtistSongsRequest, ArtistSongsResponse, ErrorResultBase,
    ArtistSongsActualRequest>
{
    public override string Url => "https://music.163.com/api/v1/artist/songs";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(ArtistSongsRequest? request)
    {
        if (request is not null)
            ActualRequest = new ArtistSongsActualRequest
                            {
                                Id = request.ArtistId,
                                OrderType = request.OrderType,
                                Offset = request.Offset,
                                Limit = request.Limit
                            };
        return Task.CompletedTask;
    }
}

public class ArtistSongsActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("private_cloud")] public bool PrivateCloud => true;
    [JsonPropertyName("work_type")] public int WorkType => 1;
    [JsonPropertyName("order")] public string OrderType { get; set; } = "hot";
    [JsonPropertyName("offset")] public int Offset { get; set; } = 0;
    [JsonPropertyName("limit")] public int Limit { get; set; } = 100;
}

public class ArtistSongsRequest : RequestBase
{
    /// <summary>
    /// 歌手 ID
    /// </summary>
    public required string ArtistId { get; set; }
    
    /// <summary>
    /// 排序类型 hot, time
    /// </summary>
    public string OrderType { get; set; } = "hot";
    
    /// <summary>
    /// 起始位置
    /// </summary>
    public int Offset { get; set; } = 0;
    
    /// <summary>
    /// 获取数量
    /// </summary>
    public int Limit { get; set; } = 200;
}

public class ArtistSongsResponse : CodedResponseBase
{
    [JsonPropertyName("total")] public bool Total { get; set; }
    [JsonPropertyName("more")] public bool HasMore { get; set; }
    [JsonPropertyName("songs")] public EmittedSongDtoWithPrivilege[]? Songs { get; set; }
}