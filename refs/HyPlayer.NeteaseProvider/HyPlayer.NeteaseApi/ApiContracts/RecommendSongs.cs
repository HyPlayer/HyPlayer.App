using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 推荐歌曲
    /// </summary>
    public static RecommendSongsApi RecommendSongsApi = new();
}


public class RecommendSongsApi : WeApiContractBase<RecommendSongsRequest, RecommendSongsResponse, ErrorResultBase, RecommendSongsActualRequest>
{
    public override string Url => "https://music.163.com/api/v3/discovery/recommend/songs";
    public override HttpMethod Method => HttpMethod.Post;

    public override Dictionary<string, string> Cookies => new() { { "os", "ios" } };

    public override string? UserAgent => "ios";

    public override Task MapRequest(RecommendSongsRequest? request)
    {
        return Task.CompletedTask;
    }
}

public class RecommendSongsRequest : RequestBase
{

}

public class RecommendSongsResponse : CodedResponseBase
{
    [JsonPropertyName("data")] public RecommendSongsData? Data { get; set; }

    public class RecommendSongsData
    {
        [JsonPropertyName("dailySongs")] public RecommendSongItem[]? DailySongs { get; set; }
    }
    
    public class RecommendSongItem : EmittedSongDtoWithPrivilege
    {
        [JsonPropertyName("recommendReason")] public string? RecommendReason { get; set; }
    }
}

public class RecommendSongsActualRequest : WeApiActualRequestBase
{

}