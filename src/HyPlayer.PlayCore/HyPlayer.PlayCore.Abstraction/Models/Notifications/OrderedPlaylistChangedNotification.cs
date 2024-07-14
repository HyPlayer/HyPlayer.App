using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Models.Notifications;

public class OrderedPlaylistChangedNotification : NotificationBase
{
    public required List<SingleSongBase> OrderedList { get; init; }
    public required bool IsRandom { get; set; }
}