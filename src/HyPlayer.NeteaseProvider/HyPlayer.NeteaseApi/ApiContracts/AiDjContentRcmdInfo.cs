using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static AiDjContentRcmdInfoApi AiDjContentRcmdInfoApi = new();
}

public class AiDjContentRcmdInfoApi : EApiContractBase<AiDjContentRcmdInfoRequest, AiDjContentRcmdInfoResponse, ErrorResultBase, AiDjContentRcmdInfoActualRequest>
{
    public override string Url => "https://interface3.music.163.com/eapi/aidj/content/rcmd/info";
    public override HttpMethod Method => HttpMethod.Post;

    public override async Task MapRequest(AiDjContentRcmdInfoRequest? request)
    {
        // TODO
    }

    public override string ApiPath => "/api/aidj/content/rcmd/info";
}

public class AiDjContentRcmdInfoRequest : RequestBase
{
    
}

public class AiDjContentRcmdInfoResponse : CodedResponseBase
{
    
}

public class AiDjContentRcmdInfoActualRequest : EApiActualRequestBase
{
    [JsonPropertyName("extInfo")] public string ExtInfo { get; set; }
}