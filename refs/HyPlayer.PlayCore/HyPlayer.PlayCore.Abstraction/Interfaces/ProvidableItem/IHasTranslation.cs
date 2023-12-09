using HyPlayer.PlayCore.Abstraction.Models;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;

public interface IHasTranslation : IProvidableItem
{
    public string? Translation { get; set; }
}