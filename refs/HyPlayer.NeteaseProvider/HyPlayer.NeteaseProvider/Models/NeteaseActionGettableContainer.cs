using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseActionGettableContainer : LinerContainerBase
{
    public NeteaseActionGettableContainer()
    {
    }

    public NeteaseActionGettableContainer(Func<Task<List<ProvidableItemBase>>> getter)
    {
        Getter = getter;
    }

    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.ActionGettableSongContainer;

    public Func<Task<List<ProvidableItemBase>>>? Getter { get; set; }

    public override async Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new())
    {
        return await (Getter?.Invoke() ?? Task.FromResult(new List<ProvidableItemBase>()));
    }
}

public class NeteaseActionGettableProgressiveContainer : NeteaseActionGettableContainer, IProgressiveLoadingContainer
{
    public NeteaseActionGettableProgressiveContainer(
        Func<int, int, Task<(bool, List<ProvidableItemBase>)>> progressiveGetter)
    {
        ProgressiveGetter = progressiveGetter;
    }

    public Func<int, int, Task<(bool, List<ProvidableItemBase>)>>? ProgressiveGetter { get; set; }


    public async Task<(bool, List<ProvidableItemBase>)> GetProgressiveItemsListAsync(int start, int count, CancellationToken ctk = new())
    {
        return await (ProgressiveGetter?.Invoke(start, count) ??
                      Task.FromResult((false, new List<ProvidableItemBase>())));
    }

    public override async Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new())
    {
        return (await GetProgressiveItemsListAsync(0, MaxProgressiveCount, ctk).ConfigureAwait(false)).Item2;
    }

    public int MaxProgressiveCount { get; set; } = 30;
}


public class NeteaseActionGettableUndetermindContainer : UndeterminedContainerBase
{
    public NeteaseActionGettableUndetermindContainer(
        Func<Task<List<ProvidableItemBase>>> progressiveGetter)
    {
        ProgressiveGetter = progressiveGetter;
    }

    public Func<Task<List<ProvidableItemBase>>>? ProgressiveGetter { get; set; }


    public override async Task<List<ProvidableItemBase>> GetNextItemsRangeAsync(CancellationToken ctk = new CancellationToken())
    {
        return await (ProgressiveGetter?.Invoke() ??
                      Task.FromResult(new List<ProvidableItemBase>()));
    }

    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.ActionGettableSongContainer;
}