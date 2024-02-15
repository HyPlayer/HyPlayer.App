using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface ICommentProvidable : IProvider
{
    public Task<ContainerBase?> GetCommentContainerAsync(CancellationToken ctk = new());
}