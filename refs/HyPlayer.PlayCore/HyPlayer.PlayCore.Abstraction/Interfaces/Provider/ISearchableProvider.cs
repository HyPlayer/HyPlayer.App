using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface ISearchableProvider : IProvider
{
    public Task<ContainerBase?> SearchProvidableItemsAsync(string keyword,string typeId, CancellationToken ctk = new());
}