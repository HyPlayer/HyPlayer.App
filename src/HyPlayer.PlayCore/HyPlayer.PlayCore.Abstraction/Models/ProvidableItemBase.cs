namespace HyPlayer.PlayCore.Abstraction.Models;

public abstract class ProvidableItemBase
{
    public required string Name { get; set; }
    public string ItemId => $"{ProviderId}{TypeId}{ActualId}";
    public abstract string ProviderId { get; }
    public abstract string TypeId { get; }
    public required string ActualId { get; set; }
}

public interface IProvidableItem { }