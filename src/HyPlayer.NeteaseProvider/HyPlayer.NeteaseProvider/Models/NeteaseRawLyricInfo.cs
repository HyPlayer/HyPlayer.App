using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Lyric;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseRawLyricInfo : RawLyricInfo
{
    public required string LyricText { get; set; }

    public bool IsWord { get; set; } = false;

    public override LyricType LyricType => LyricTypeActual;
    
    public required LyricType LyricTypeActual { get; set; }

    public LyricAuthorInfo? Author { get; set; }
    
    public class LyricAuthorInfo : ProvidableItemBase
    {
        public override string ProviderId => "ncm";
        public override string TypeId => NeteaseTypeIds.User;
        
    }

    public override Task<object?> GetResourceAsync(ResourceQualityTag? qualityTag = null, Type? awaitingType = null,CancellationToken ctk = new())
    {
        return Task.FromResult<object?>(LyricText);
    }
}