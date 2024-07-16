using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models;
using System;

namespace HyPlayer.Extensions;

public static class CoverExtensions
{
    public static Uri? GetCoverUrl(this IHasCover item, int pixelX, int pixelY)
    {
        var coverResource = item.GetCoverAsync(new NeteaseImageResourceQualityTag(pixelX, pixelY)).GetAwaiter().GetResult();
        if (coverResource is not NeteaseImageResourceResult neteaseResourceResult)
            return null;
        return neteaseResourceResult
                           .GetResourceAsync()
                           .GetAwaiter().GetResult();
    }
}