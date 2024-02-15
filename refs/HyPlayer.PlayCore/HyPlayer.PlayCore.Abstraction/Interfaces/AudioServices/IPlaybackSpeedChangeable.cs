using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

public interface IPlaybackSpeedChangeable : IAudioService
{
    /// <summary>
    /// 允许更改播放更改播放倍速 (x1.0)
    /// </summary>
    public interface IPlaybackRateChangeableService
    {
        /// <summary>
        /// 更改 AudioService 播放倍速
        /// </summary>
        /// <param name="ticket">音频票</param>
        /// <param name="playbackSpeed">目标速度(x1.0)</param>
        /// <returns></returns>
        public Task ChangePlaybackSpeedAsync(AudioTicketBase ticket, double playbackSpeed, CancellationToken ctk = new());
    }
}