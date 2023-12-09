using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;
public static partial class NeteaseApis
{
    /// <summary>
    /// 歌单歌曲编辑
    /// </summary>
    public static PlaylistTracksEditApi PlaylistTracksEditApi = new();
}

public class PlaylistTracksEditApi : WeApiContractBase<PlaylistTracksEditRequest, PlaylistTracksEditResponse, ErrorResultBase, PlaylistTracksEditActualRequest>
{
    public override string Url => "https://music.163.com/weapi/playlist/manipulate/tracks";
    public override HttpMethod Method => HttpMethod.Post;
    public override Task MapRequest(PlaylistTracksEditRequest? request)
    {
        if (request is null) return Task.CompletedTask;
        var trackIds = request.TrackIds is not null ? $"[{string.Join(",", request.TrackIds)}]" : request.TrackId!;

        ActualRequest = new PlaylistTracksEditActualRequest
                        {
                            Operation = request.IsAdd ? "add" : "del",
                            PlaylistId = request.PlaylistId,
                            TrackIds = trackIds
                        };
        return Task.CompletedTask;
    }
}

public class PlaylistTracksEditActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("op")] public required string Operation { get; set; }
    [JsonPropertyName("pid")] public required string PlaylistId { get; set; }
    [JsonPropertyName("trackIds")] public required string TrackIds { get; set; }
    [JsonPropertyName("imme")] public bool Imme => true;

}

public class PlaylistTracksEditRequest : RequestBase
{
    /// <summary>
    /// 是否添加
    /// </summary>
    public bool IsAdd { get; set; } = true;
    
    /// <summary>
    /// 歌单 ID
    /// </summary>
    public required string PlaylistId { get; set; }
    
    /// <summary>
    /// 歌曲 ID
    /// </summary>
    public string? TrackId { get; set; }
    
    /// <summary>
    /// 歌曲 ID 列表
    /// </summary>
    public string[]? TrackIds { get; set; }
}
public class PlaylistTracksEditResponse : CodedResponseBase
{
    
}