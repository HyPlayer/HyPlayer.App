using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore;

public sealed partial class Chopin
{
    public override bool IsRandom { get; protected set; }

    public override ContainerBase? CurrentSongContainer { get; protected set; }

    public override PlayListManagerBase? CurrentPlayList { get; protected set; }


    public override Task ChangeSongContainerAsync(ContainerBase? container, CancellationToken ctk = new())
    {
        CurrentSongContainer = container;
        return Task.CompletedTask;
    }

    public override async Task InsertSongAsync(SingleSongBase item, int index = -1, CancellationToken ctk = new())
    {
        await (CurrentPlayList?.AddSongAsync(item, index, ctk) ?? Task.CompletedTask);
    }

    public override async Task InsertSongRangeAsync(List<SingleSongBase> items, int index = -1, CancellationToken ctk = new())
    {
        await (CurrentPlayList?.AddSongRangeAsync(items, index, ctk) ?? Task.CompletedTask);
    }

    public override async Task RemoveSongAsync(SingleSongBase item, CancellationToken ctk = new())
    {
        await (CurrentPlayList?.RemoveSongAsync(item, ctk) ?? Task.CompletedTask);
    }

    public override async Task RemoveSongRangeAsync(List<SingleSongBase> items, CancellationToken ctk = new())
    {
        await (CurrentPlayList?.RemoveSongRangeAsync(items, ctk) ?? Task.CompletedTask);
    }

    public override async Task RemoveAllSongAsync(CancellationToken ctk = new())
    {
        await (CurrentPlayList?.ClearSongsAsync(ctk) ?? Task.CompletedTask);
    }

    public override async Task SetRandomAsync(bool isRandom, CancellationToken ctk = new())
    {
        if (CurrentSongContainer is IRandomizablePlayListController randomizablePlayListController)
            await randomizablePlayListController.RandomizeAsync(isRandom ? -1 : DateTime.Now.Millisecond, ctk);
    }

    public override async Task ReRandomAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayListController is IRandomizablePlayListController randomizablePlayListController)
            await randomizablePlayListController.RandomizeAsync(DateTime.Now.Millisecond, ctk);
    }
}