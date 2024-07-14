using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface IRecommendationProvidable : IProvider
{
    public Task<ContainerBase?> GetRecommendationAsync(string? typeId = null, CancellationToken ctk = new());
}