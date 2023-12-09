namespace HyPlayer.PlayCore.Abstraction.Models.Containers;

public abstract class UndeterminedContainerBase : ContainerBase
{
    public abstract Task<List<ProvidableItemBase>> GetNextItemsRangeAsync(CancellationToken ctk = new());
}