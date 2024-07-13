namespace HyPlayer.PlayCore.Abstraction.Models.Lyric;

public class ProcessedLyricLine : ILyricLine
{
    public required long StartTime { get; set; }
    public required long EndTime { get; set; }
    public string? Text { get; set; }
}

public interface ILyricLine { }