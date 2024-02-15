using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.Extensions;

public static class CoverExtensions
{
    public static string? GetCoverUrl(this IHasCover item, int pixelX, int pixelY)
    {
        var coverResource = item.GetCoverAsync(new ImageResourceQualityTag(pixelX, pixelY)).GetAwaiter().GetResult();
        if (coverResource is not IResourceResultOf<string> resourceResult)
            return null;
        return resourceResult
                           .GetResourceAsync()
                           .GetAwaiter().GetResult();
    }
}