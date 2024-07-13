using System.Collections.ObjectModel;
using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;

public interface IProgressiveLoadingContainer : IContainer
{
    public int MaxProgressiveCount { get; }
    public Task<(bool, List<ProvidableItemBase>)> GetProgressiveItemsListAsync(int start, int count, CancellationToken ctk = new());
}