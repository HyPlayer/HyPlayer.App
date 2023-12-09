using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static PlaylistTracksGetApi PlaylistTracksGetApi = new();
}

public class PlaylistTracksGetApi : EApiContractBase<PlaylistTracksGetRequest, PlaylistTracksGetResponse, ErrorResultBase, PlaylistTracksGetActualRequest>
{
    public override string Url => "https://interface.music.163.com/eapi/v6/playlist/detail";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(PlaylistTracksGetRequest? request)
    {
        if (request is not null)
            ActualRequest = new PlaylistTracksGetActualRequest()
                            {
                                Id = request.Id
                            };
        return Task.CompletedTask;
    }

    public override string ApiPath => "/api/v6/playlist/detail";
}

public class PlaylistTracksGetRequest : RequestBase
{
    public required string Id { get; set; }
}

public class PlaylistTracksGetResponse : CodedResponseBase
{
    [JsonPropertyName("playlist")] public PlaylistWithTracksInfoDto Playlist { get; set; }

    public class PlaylistWithTracksInfoDto : PlaylistDetailResponse.PlayListData
    {
        [JsonPropertyName("trackIds")] public TrackIdItem[]? TrackIds { get; set; }
        
        public class TrackIdItem
        {
            [JsonPropertyName("id")] public required string Id { get; set; }
            [JsonPropertyName("rcmdReason")] public string? RecommendReason { get; set; }
            [JsonPropertyName("at")] public long? AddedAt { get; set; }
            [JsonPropertyName("uid")] public string? AddedBy { get; set; }
        }
    }
}

public class PlaylistTracksGetActualRequest : EApiActualRequestBase
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("s")] public int S { get; set; } = 0;
    [JsonPropertyName("n")] public int N { get; set; } = 0;

}