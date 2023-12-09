using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;

public interface IHasCover : IProvidableItem
{
    public Task<ImageResourceBase?> GetCoverAsync( CancellationToken ctk = new());
}