namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions.Notifications
{
    public class AudioGraphPlaybackPositionChangedNotification:PlaybackPositionChangedNotification
    {
        public override double CurrentPlaybackPosition { get; init; }
        public AudioGraphPlaybackPositionChangedNotification(double currentPlaybackPosition)
        {
            CurrentPlaybackPosition = currentPlaybackPosition;
        }
    }
}
