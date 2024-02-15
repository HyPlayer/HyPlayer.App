using Depository.Abstraction.Interfaces.NotificationHub;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;

namespace HyPlayer.PlayCore;

public sealed partial class Chopin :
    INotificationSubscriber<CurrentSongChangedNotification>
{
    public Task HandleNotificationAsync(CurrentSongChangedNotification notification, CancellationToken ctk = new CancellationToken())
    {
        ctk.ThrowIfCancellationRequested();
        CurrentSong = notification.CurrentPlayingSong;
        return Task.CompletedTask;
    }
}