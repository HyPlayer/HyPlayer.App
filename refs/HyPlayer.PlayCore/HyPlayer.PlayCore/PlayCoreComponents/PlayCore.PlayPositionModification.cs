using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore;

public sealed partial class Chopin
{
    public override async Task MovePointerToAsync(SingleSongBase song, CancellationToken ctk = new())
    {
        if (CurrentPlayListController is INavigateSongPlayListController controller)
            await controller.NavigateSongToAsync(song, ctk);
    }

    public override async Task MoveNextAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayListController is { } controller)
            await controller.MoveNextAsync(ctk);
    }

    public override async Task MovePreviousAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayListController is { } controller)
            await controller.MovePreviousAsync(ctk);
    }
}