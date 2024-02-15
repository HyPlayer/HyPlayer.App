using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;

public interface IRandomizablePlayListController : IPlaylistController
{
    public Task RandomizeAsync(int seed = -1, CancellationToken ctk = new());
    public Task<List<SingleSongBase>> GetOriginalListAsync(CancellationToken ctk = new());
}