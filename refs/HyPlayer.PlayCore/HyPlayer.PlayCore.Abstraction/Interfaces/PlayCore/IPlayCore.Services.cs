namespace HyPlayer.PlayCore.Abstraction.Interfaces.PlayCore;

public interface IPlayCoreService : IPlayCore
{
    public Task RegisterAudioServiceAsync(Type serviceType, CancellationToken ctk = new());
    public Task RegisterMusicProviderAsync(Type serviceType, CancellationToken ctk = new());
    public Task RegisterPlayListControllerAsync(Type serviceType, CancellationToken ctk = new());

    public Task UnregisterAudioServiceAsync(Type serviceType, CancellationToken ctk = new());
    public Task UnregisterMusicProviderAsync(Type serviceType, CancellationToken ctk = new());
    public Task UnregisterPlayListControllerAsync(Type serviceType, CancellationToken ctk = new());

    public Task FocusAudioServiceAsync(Type serviceType, CancellationToken ctk = new());
    public Task FocusPlayListControllerAsync(Type serviceType, CancellationToken ctk = new());
}