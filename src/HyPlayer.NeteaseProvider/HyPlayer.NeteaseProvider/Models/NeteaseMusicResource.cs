using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseMusicResource : MusicResourceBase
{
    public string? Md5 { get; set; }
    public long Size { get; set; }
    public string? BitRate { get; set; }
    public string? EncodeType { get; set; }
    public long? Time { get; set; }
    public string? MusicType { get; set; }
    public string? Level { get; set; }


    public override Task<object?> GetResourceAsync(ResourceQualityTag? qualityTag = null, Type? awaitingType = null,CancellationToken ctk = new())
    {
        return Task.FromResult<object?>(Url);
    }
}