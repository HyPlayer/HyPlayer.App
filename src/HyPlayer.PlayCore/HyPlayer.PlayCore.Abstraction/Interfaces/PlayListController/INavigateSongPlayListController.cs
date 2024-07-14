using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;

public interface INavigateSongPlayListController : IPlaylistController
{
    public abstract Task NavigateSongToAsync(SingleSongBase song, CancellationToken ctk = new());
}