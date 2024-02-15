using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IStopAudioTicketService : IAudioService
{
    public Task StopTicketAsync(AudioTicketBase ticket, CancellationToken ctk = new());
}