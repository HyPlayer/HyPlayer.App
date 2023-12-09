using System.Collections.ObjectModel;

namespace HyPlayer.PlayCore.Abstraction.Models.Containers;

public abstract class LinerContainerBase : ContainerBase
{
    public abstract Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new());
}