using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayCore;

public interface IPlayCorePlayPositionModification : IPlayCore
{
    public Task MovePointerToAsync(SingleSongBase song, CancellationToken ctk = new());
    public Task MoveNextAsync(CancellationToken ctk = new());
    public Task MovePreviousAsync(CancellationToken ctk = new());
}