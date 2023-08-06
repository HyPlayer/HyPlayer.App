using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.App.Extensions;

public static class CoverExtensions
{
    public static string? GetCoverUrl(this IHasCover item, int pixelX, int pixelY)
    {
        return (string?)item.GetCoverAsync().GetAwaiter().GetResult()?
                           .GetResourceAsync(new ImageResourceQualityTag(pixelX, pixelY), typeof(string))
                           .GetAwaiter().GetResult();
    }
}