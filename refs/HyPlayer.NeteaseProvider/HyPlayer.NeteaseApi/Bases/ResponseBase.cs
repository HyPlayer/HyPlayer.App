namespace HyPlayer.NeteaseApi.Bases;

public class ResponseBase
{
#if DEBUG
    public string? OriginalResponse { get; set; }
#endif
}