using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace NAudioAudioService;

public class NAudioAudioService : AudioServiceBase
{
    public override async Task<AudioTicketBase> GetAudioTicketAsync(MusicResourceBase musicResource,
                                                                    CancellationToken ctk = new())
    {
        throw new NotImplementedException();
    }

    public override async Task DisposeAudioTicketAsync(AudioTicketBase audioTicket, CancellationToken ctk = new())
    {
        throw new NotImplementedException();
    }


    public override async Task<List<AudioTicketBase>> GetCreatedAudioTicketsAsync(CancellationToken ctk = new())
    {
        throw new NotImplementedException();
    }

    public override string Id => "nad";
    public override string Name => "NAudio Service";
}