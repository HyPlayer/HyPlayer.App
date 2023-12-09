using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 删除云盘歌曲
    /// </summary>
    public static UserCloudDeleteApi UserCloudDeleteApi = new();
}

public class UserCloudDeleteApi : WeApiContractBase<UserCloudDeleteRequest, UserCloudDeleteResponse, ErrorResultBase,
    UserCloudDeleteActualRequest>
{
    public override string Url => "https://music.163.com/weapi/cloud/del";
    public override HttpMethod Method => HttpMethod.Post;
    public override string? UserAgent => "pc";
    public override Dictionary<string, string> Cookies => new() { { "os", "pc" }, { "appver", "2.7.1.198277" } };

    public override Task MapRequest(UserCloudDeleteRequest? request)
    {
        if (request is null) return Task.CompletedTask;
        var ids = string.IsNullOrWhiteSpace(request.Id) ? $"[{string.Join(",", request.IdList!)}]" : $"[{request.Id}]";
        ActualRequest = new UserCloudDeleteActualRequest()
                        {
                            SongIds = ids
                        };
        return Task.CompletedTask;
    }
}

public class UserCloudDeleteRequest : RequestBase
{
    /// <summary>
    /// 歌曲 ID
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// 歌曲 ID 列表
    /// </summary>
    public string[]? IdList { get; set; }
}

public class UserCloudDeleteResponse : CodedResponseBase
{
}

public class UserCloudDeleteActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("songIds")] public required string SongIds { get; set; }
}