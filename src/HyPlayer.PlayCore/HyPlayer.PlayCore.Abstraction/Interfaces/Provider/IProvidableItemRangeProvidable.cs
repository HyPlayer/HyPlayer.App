using System.Collections.ObjectModel;
using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface IProvidableItemRangeProvidable : IProvider
{
    public Task<List<ProvidableItemBase>> GetProvidableItemsRangeAsync(List<string> inProviderIds, CancellationToken ctk = new());
}