using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseImageResource : ImageResourceBase, IResourceResultOf<string>
{
    public Task<string?> GetResourceAsync(CancellationToken cancellationToken = default)
    {
        if (QualityTag is ImageResourceQualityTag irqt)
            return Task.FromResult($"{Url}?param={irqt.PixelX}y{irqt.PixelY}");
        else
        {
            return Task.FromResult($"{Url}");
        }
    }
}