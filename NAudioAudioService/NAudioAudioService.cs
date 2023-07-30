using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace NAudioAudioService;

public class NAudioAudioService : AudioServiceBase
{
    public override async Task<AudioTicketBase> GetAudioTicket(MusicResourceBase musicResource)
    {
        throw new NotImplementedException();
    }

    public override async Task DisposeAudioTicket(AudioTicketBase audioTicket)
    {
        throw new NotImplementedException();
    }


    public override async Task<List<AudioTicketBase>> GetCreatedAudioTickets()
    {
        throw new NotImplementedException();
    }

    public override string Id => "na";
    public override string Name => "NAudio Service";
}