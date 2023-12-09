using System.Collections.ObjectModel;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface IProvableItemLikable : IProvider
{
    public Task LikeProvidableItemAsync(string inProviderId, string? targetId, CancellationToken ctk = new());
    public Task UnlikeProvidableItemAsync(string inProviderId, string? targetId, CancellationToken ctk = new());
    public Task<List<string>> GetLikedProvidableIdsAsync(string typeId, CancellationToken ctk = new());
}