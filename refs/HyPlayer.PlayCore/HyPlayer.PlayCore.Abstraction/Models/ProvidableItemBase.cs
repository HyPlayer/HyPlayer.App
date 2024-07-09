namespace HyPlayer.PlayCore.Abstraction.Models;

public abstract class ProvidableItemBase
{
    public string Name { get; set; } = "";
    public string ItemId => $"{ProviderId}{TypeId}{ActualId}";
    public abstract string ProviderId { get; }
    public abstract string TypeId { get; }
    public string ActualId { get; set; } = "0000";
}

public interface IProvidableItem { }