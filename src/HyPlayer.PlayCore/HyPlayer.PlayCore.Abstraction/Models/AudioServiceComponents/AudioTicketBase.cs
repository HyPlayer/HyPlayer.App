using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

public abstract class AudioTicketBase
{
    public required AudioTicketStatus Status { get; set; } = AudioTicketStatus.None;
    public required string AudioServiceId { get; init; }
    public required MusicResourceBase MusicResource { get; init; }
}

public enum AudioTicketStatus
{
    None,
    Buffering,
    Playing,
    Paused,
    Stopped,
    Failed,
}