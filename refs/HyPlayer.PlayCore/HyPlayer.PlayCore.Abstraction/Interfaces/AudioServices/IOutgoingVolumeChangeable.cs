namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IOutgoingVolumeChangeable : IAudioService
{
    public Task ChangeOutgoingVolume(double volume, CancellationToken ctk = new());
}