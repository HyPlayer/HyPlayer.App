using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;

public interface IIndexedPlayListController : IPlaylistController
{
    public Task<int> GetCurrentIndexAsync( CancellationToken ctk = new());
    public Task<SingleSongBase?> GetSongAtAsync(int index, CancellationToken ctk = new());
}