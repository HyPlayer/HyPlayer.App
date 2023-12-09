using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseApi.Bases.ApiContractBases;
using HyPlayer.NeteaseApi.Models.ResponseModels;

namespace HyPlayer.NeteaseApi.ApiContracts;

public static partial class NeteaseApis
{
    public static CommentFloorApi CommentFloorApi = new();
}

public class CommentFloorApi : WeApiContractBase<CommentFloorRequest, CommentFloorResponse, ErrorResultBase,
    CommentFloorActualRequest>
{
    public override string Url => "https://music.163.com/api/resource/comment/floor/get";
    public override HttpMethod Method => HttpMethod.Post;

    public override Task MapRequest(CommentFloorRequest? request)
    {
        if (request is not null)
            ActualRequest = new()
                            {
                                CsrfToken = null,
                                ThreadId = request.ThreadId,
                                ParentCommentId = request.ParentCommentId,
                                Time = request.Time,
                                Limit = request.Limit
                            };
        return Task.CompletedTask;
    }
}

public class CommentFloorRequest : RequestBase
{
    /// <summary>
    /// 评论区 ID (type + id)
    /// 使用 
    /// </summary>
    public required string ThreadId { get; set; }

    /// <summary>
    /// 父评论 ID
    /// </summary>
    public required string ParentCommentId { get; set; }

    /// <summary>
    /// 分页参数 - 最后一项的 time
    /// </summary>
    public long Time { get; set; } = -1;

    /// <summary>
    /// 评论数量
    /// </summary>
    public int Limit { get; set; } = 20;
}

public class CommentFloorResponse : CodedResponseBase
{
    [JsonPropertyName("data")] public CommentFloorData? Data { get; set; }

    public class CommentFloorData
    {
        [JsonPropertyName("time")] public long Time { get; set; }
        [JsonPropertyName("hasMore")] public bool HasMore { get; set; }
        [JsonPropertyName("comments")] public CommentDto[]? Comments { get; set; }
        [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
    }
}

public class CommentFloorActualRequest : WeApiActualRequestBase
    {
        [JsonPropertyName("threadId")] public required string ThreadId { get; set; }
        [JsonPropertyName("parentCommentId")] public required string ParentCommentId { get; set; }
        [JsonPropertyName("time")] public long Time { get; set; } = -1;
        [JsonPropertyName("limit")] public int Limit { get; set; } = 20;
    }