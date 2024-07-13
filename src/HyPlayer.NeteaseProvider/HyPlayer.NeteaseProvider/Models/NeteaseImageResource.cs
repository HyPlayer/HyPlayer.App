using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseImageResource : ImageResourceBase
{
    public override async Task<object?> GetResourceAsync(ResourceQualityTag? qualityTag = null, Type? awaitingType = null,CancellationToken ctk = new())
    {
        if (awaitingType == typeof(string))
        {
            return Url;
        }

        return null;
    }
}