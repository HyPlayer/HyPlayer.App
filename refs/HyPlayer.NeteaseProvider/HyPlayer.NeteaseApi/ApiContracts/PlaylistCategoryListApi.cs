using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static PlaylistCategoryListApi PlaylistCategoryListApi = new();
}

public class PlaylistCategoryListApi : EApiContractBase<PlaylistCategoryListRequest, PlaylistCategoryListResponse, ErrorResultBase, PlaylistCategoryListActualRequest>
{
    public override string Url => "https://interface.music.163.com/eapi/playlist/category/list";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(PlaylistCategoryListRequest? request)
    {
        if (request is not null)
            ActualRequest = new()
                            {
                                Category = request.Category,
                                Limit = request.Limit
                            };
        return Task.CompletedTask;
    }

    public override string ApiPath => "/api/playlist/category/list";
}

public class PlaylistCategoryListRequest : RequestBase
{
    public required string Category { get; set; }
    public int Limit { get; set; } = 6;
}

public class PlaylistCategoryListResponse : CodedResponseBase
{
    [JsonPropertyName("playlists")] public PlaylistDto[]? Playlists { get; set; }
    [JsonPropertyName("playlistIds")] public string[]? PlaylistIds { get; set; }
}

public class PlaylistCategoryListActualRequest : EApiActualRequestBase
{
    [JsonPropertyName("cat")] public required string Category { get; set; }
    [JsonPropertyName("limit")] public int Limit { get; set; } = 6;
}