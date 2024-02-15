namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions.Notifications
{
    public abstract class PlaybackPositionChangedNotification
    {
        public abstract double CurrentPlaybackPosition { get; init; }
    }
}
