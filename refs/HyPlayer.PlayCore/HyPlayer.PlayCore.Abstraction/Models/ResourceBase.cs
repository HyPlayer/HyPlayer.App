using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.PlayCore.Abstraction.Models;

public abstract class ResourceBase
{
    public Uri? Uri { get; set; }
    public string? ResourceName { get; set; }
    public bool HasContent { get; set; }
    public string? ExtensionName { get; set; }
    public abstract ResourceType Type { get; }
    public ResourceQualityTag? QualityTag { get; set; }
    public string? Url { get; set; }
}
public enum ResourceType
{
    None,
    Image,
    Text,
    Video,
    Audio,
    Binary,
}

public enum ResourceStatus
{
    Success,
    Fail
}

public interface IResourceResultOf<T>
{
    public Task<T?> GetResourceAsync(CancellationToken cancellationToken = default);
}