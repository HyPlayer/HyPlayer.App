using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Lyric;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseRawLyricInfo : RawLyricInfo, IResourceResultOf<string>
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

    public Task<string?> GetResourceAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(LyricText)!;
    }
}