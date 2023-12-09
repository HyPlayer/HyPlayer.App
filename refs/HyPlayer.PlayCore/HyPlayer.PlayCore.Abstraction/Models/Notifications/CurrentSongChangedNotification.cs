using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Models.Notifications;

public class CurrentSongChangedNotification : NotificationBase
{
    public SingleSongBase? CurrentPlayingSong { get; set; }
}