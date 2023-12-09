using System.Collections.ObjectModel;

namespace HyPlayer.PlayCore.Abstraction.Models.Containers;

public abstract class ContainersContainer : ContainerBase
{
    public abstract Task<List<ContainerBase>> GetSubContainerAsync(CancellationToken ctk = new());
}