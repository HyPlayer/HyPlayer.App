using System.Collections.ObjectModel;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction;

public abstract class PlayControllerBase : IPlaylistController
{
    public abstract Task<SingleSongBase?> MoveNextAsync(CancellationToken ctk = new());
    public abstract Task<SingleSongBase?> MovePreviousAsync(CancellationToken ctk = new());
    public abstract Task<SingleSongBase?> MoveToIndexAsync(int index, CancellationToken ctk = new());
}

public interface IPlaylistController { }