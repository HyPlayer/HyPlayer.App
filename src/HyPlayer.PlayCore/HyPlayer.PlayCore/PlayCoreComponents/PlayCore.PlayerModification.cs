using HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore;

public sealed partial class Chopin
{
    public override AudioTicketBase? CurrentPlayingTicket { get; protected set; }

    public override async Task SeekAsync(long position, CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IAudioTicketSeekableService seekableService)
                await seekableService.SeekAudioTicket(CurrentPlayingTicket!, position, ctk);
    }

    public override async Task PlayAsync( CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IPlayAudioTicketService playableService)
                await playableService.PlayAudioTicket(CurrentPlayingTicket!, ctk);
    }

    public override async Task PauseAsync( CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IPauseAudioTicketService pauseService)
                await pauseService.PauseAudioTicket(CurrentPlayingTicket!, ctk);
    }

    public override async Task StopAsync( CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IStopAudioTicketService stopService)
                await stopService.StopTicket(CurrentPlayingTicket!, ctk);
    }
}