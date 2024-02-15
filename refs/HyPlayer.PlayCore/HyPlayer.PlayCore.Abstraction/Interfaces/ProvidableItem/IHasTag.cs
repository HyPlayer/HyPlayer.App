using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;

public interface IHasTag : IProvidableItem
{
    public List<string> Tags { get; set; }
}