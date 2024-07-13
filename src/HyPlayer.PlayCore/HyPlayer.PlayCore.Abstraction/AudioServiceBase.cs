using System.Collections.ObjectModel;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.PlayCore.Abstraction;

public abstract class AudioServiceBase : IAudioService
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract Task<AudioTicketBase> GetAudioTicketAsync(MusicResourceBase musicResource, CancellationToken ctk = new());
    public abstract Task DisposeAudioTicketAsync(AudioTicketBase audioTicket, CancellationToken ctk = new());
    public abstract Task<List<AudioTicketBase>> GetCreatedAudioTicketsAsync(CancellationToken ctk = new());
}

public interface IAudioService { }