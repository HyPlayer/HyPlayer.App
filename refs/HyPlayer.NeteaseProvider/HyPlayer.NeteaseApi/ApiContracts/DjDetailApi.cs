using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    /// <summary>
    /// 电台详情
    /// </summary>
    public static DjDetailApi DjDetailApi = new();
}

public class DjDetailApi : WeApiContractBase<DjDetailRequest, DjDetailResponse, ErrorResultBase, DjDetailActualRequest>
{
    public override string Url => "https://music.163.com/weapi/djDj/get";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(DjDetailRequest? request)
    {
        ActualRequest = new DjDetailActualRequest
                        {
                            Id = request!.Id
                        };
        return Task.CompletedTask;
    }
}

public class DjDetailRequest : RequestBase
{
    /// <summary>
    /// 电台 ID
    /// </summary>
    public required string Id { get; set; }
}

public class DjDetailResponse : CodedResponseBase
{
    [JsonPropertyName("data")] public DjDetailData? RadioData { get; set; }

    public class DjDetailData : DjRadioDto
    {
        [JsonPropertyName("dj")] public UserInfoDto? DjData { get; set; }
    }
}

public class DjDetailActualRequest : WeApiActualRequestBase
{
    [JsonPropertyName("id")] public required string Id { get; set; }
}