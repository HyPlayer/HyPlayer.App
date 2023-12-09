using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IPauseAudioTicketService : IAudioService
{
    public Task PauseAudioTicket(AudioTicketBase ticket, CancellationToken ctk = new());
}