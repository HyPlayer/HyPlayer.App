namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;

public interface IReversiblePlayListController : IPlaylistController
{
    public Task Reverse(CancellationToken ctk = new());
}