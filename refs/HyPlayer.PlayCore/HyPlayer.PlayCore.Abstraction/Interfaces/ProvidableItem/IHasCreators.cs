using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;

public interface IHasCreators : IProvidableItem
{
    public List<string>? CreatorList { get; init; }
    public Task<List<PersonBase>?> GetCreatorsAsync(CancellationToken ctk = new());
}