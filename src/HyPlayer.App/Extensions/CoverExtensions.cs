using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.App.Extensions;

public static class CoverExtensions
{
    public static string GetCoverUrl(this NeteasePlaylist item, int pixelX, int pixelY)
    {
        return (string)item.GetCover().GetAwaiter().GetResult()
                           .GetResource(new ImageResourceQualityTag(pixelX, pixelY), typeof(string))
                           .GetAwaiter().GetResult();
    }
}