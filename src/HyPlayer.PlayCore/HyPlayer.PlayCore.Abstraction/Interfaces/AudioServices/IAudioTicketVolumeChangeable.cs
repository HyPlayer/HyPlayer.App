using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IAudioTicketVolumeChangeable : IAudioService
{
    public Task ChangeVolumeAsync(AudioTicketBase ticket, double volume, CancellationToken ctk = new());
}