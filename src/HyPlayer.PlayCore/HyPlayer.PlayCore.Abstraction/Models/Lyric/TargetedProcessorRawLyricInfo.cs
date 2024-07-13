namespace HyPlayer.PlayCore.Abstraction.Models.Lyric;

public abstract class TargetedProcessorRawLyricInfo : RawLyricInfo
{
    public abstract Type TargetedProcessor { get; }
}