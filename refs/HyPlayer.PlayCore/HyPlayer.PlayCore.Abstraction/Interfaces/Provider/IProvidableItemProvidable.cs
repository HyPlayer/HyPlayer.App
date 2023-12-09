using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface IProvidableItemProvidable : IProvider
{
    public Task<ProvidableItemBase?> GetProvidableItemByIdAsync(string inProviderId, CancellationToken ctk = new());
}