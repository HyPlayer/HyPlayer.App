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
                await seekableService.SeekAudioTicketAsync(CurrentPlayingTicket!, position, ctk).ConfigureAwait(false);
    }

    public override async Task PlayAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IPlayAudioTicketService playableService)
                await playableService.PlayAudioTicketAsync(CurrentPlayingTicket!, ctk).ConfigureAwait(false);
    }

    public override async Task PauseAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IPauseAudioTicketService pauseService)
                await pauseService.PauseAudioTicketAsync(CurrentPlayingTicket!, ctk).ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken ctk = new())
    {
        if (CurrentPlayingTicket is null) return;
        if (CurrentPlayingTicket?.AudioServiceId == CurrentAudioService?.Id)
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (CurrentAudioService is IStopAudioTicketService stopService)
                await stopService.StopTicketAsync(CurrentPlayingTicket!, ctk).ConfigureAwait(false);
    }
}