namespace HyPlayer.PlayCore.Abstraction.Models.Lyric;

public class LyricWord
{
    public string? Word { get; set; }
    public required long StartTime { get; set; }
    public required long EndTime { get; set; }
}