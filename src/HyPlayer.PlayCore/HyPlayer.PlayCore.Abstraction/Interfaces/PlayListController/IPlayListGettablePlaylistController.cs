using System.Collections.ObjectModel;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;

public interface IPlayListGettablePlaylistController : IPlaylistController
{
    public Task<List<SingleSongBase>> GetOrderedPlayListAsync( CancellationToken ctk = new());
}