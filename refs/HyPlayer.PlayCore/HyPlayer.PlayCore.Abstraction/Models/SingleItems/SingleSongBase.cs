using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.PlayCore.Abstraction.Models.SingleItems;

public abstract class SingleSongBase : ProvidableItemBase, IHasCreators
{
    public AlbumBase? Album { get; set; }
    public List<string>? CreatorList { get; init; }
    public abstract Task<List<PersonBase>?> GetCreatorsAsync(CancellationToken ctk = new());
    public long Duration { get; set; }
    public bool Available { get; set; }
}