using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IPlayAudioTicketService : IAudioService
{
    public Task PlayAudioTicket(AudioTicketBase ticket, CancellationToken ctk = new());
}