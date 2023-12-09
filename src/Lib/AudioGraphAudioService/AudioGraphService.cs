using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depository.Abstraction.Interfaces;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace AudioGraphAudioService;

public class AudioGraphService : 
    AudioServiceBase,
    IAudioTicketListProvidable,
    IAudioTicketSeekableService,
    IAudioTicketVolumeChangeable,
    IOutgoingVolumeChangeable,
    IOutputDeviceChangeableService,
    IPauseAudioTicketService,
    IPlayAudioTicketService,
    IPlaybackSpeedChangeable,
    IStopAudioTicketService
{

    private INotificationHub _notificationHub;
    
    public AudioGraphService(INotificationHub notificationHub)
    {
        _notificationHub = notificationHub;
    }
    
    public override string Id => "agp";
    public override string Name => "AudioGraph";
    
    public override async Task<AudioTicketBase> GetAudioTicketAsync(MusicResourceBase musicResource, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override async Task DisposeAudioTicketAsync(AudioTicketBase audioTicket, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override async Task<List<AudioTicketBase>> GetCreatedAudioTicketsAsync(CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }


    public async Task<List<AudioTicketBase>> GetAudioTicketListAsync(CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task SeekAudioTicket(AudioTicketBase audioTicket, long position, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task ChangeVolume(AudioTicketBase ticket, double volume, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task ChangeOutgoingVolume(double volume, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task<List<OutputDeviceBase>> GetOutputDevices(CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task SetOutputDevices(OutputDeviceBase device, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task PauseAudioTicket(AudioTicketBase ticket, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task PlayAudioTicket(AudioTicketBase ticket, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public async Task StopTicket(AudioTicketBase ticket, CancellationToken ctk = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }
}