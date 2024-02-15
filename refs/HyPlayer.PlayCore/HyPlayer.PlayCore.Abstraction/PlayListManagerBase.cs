using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction;

public abstract class PlayListManagerBase
{
    public abstract Task AddSongContainerAsync(ContainerBase container, CancellationToken ctk = new());
    public abstract Task RemoveSongContainerAsync(ContainerBase container, CancellationToken ctk = new());
    public abstract Task<List<ContainerBase>> GetAllSongContainersAsync(CancellationToken ctk = new());
    public abstract Task ClearSongContainersAsync(CancellationToken ctk = new());

    public abstract Task<List<SingleSongBase>> GetPlayListAsync(CancellationToken ctk = new());
    public abstract Task AddSongAsync(SingleSongBase song, int index = -1, CancellationToken ctk = new());
    public abstract Task AddSongRangeAsync(List<SingleSongBase> song, int index = -1, CancellationToken ctk = new());
    public abstract Task RemoveSongAsync(SingleSongBase song, CancellationToken ctk = new());
    public abstract Task RemoveSongRangeAsync(List<SingleSongBase> song, CancellationToken ctk = new());
    public abstract Task ClearSongsAsync(CancellationToken ctk = new());

    public abstract Task SetSongListAsync(List<SingleSongBase> song, CancellationToken ctk = new());
}