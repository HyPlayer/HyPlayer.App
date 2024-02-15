using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IAudioTicketListProvidable : IAudioService
{
    public abstract Task<List<AudioTicketBase>> GetAudioTicketListAsync(CancellationToken ctk = new());
}