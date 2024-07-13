using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Models.Notifications;

public class InnerPlayListProgressiveAddedNotification : NotificationBase
{
    public required List<SingleSongBase> AddedSongs { get; init; }
}