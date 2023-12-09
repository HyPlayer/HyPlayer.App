namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayCore;

public interface IPlayCorePlayerModification : IPlayCore
{
    public Task SeekAsync(long position, CancellationToken ctk = new());
    public Task PlayAsync(CancellationToken ctk = new());
    public Task PauseAsync(CancellationToken ctk = new());
    public Task StopAsync(CancellationToken ctk = new());
}