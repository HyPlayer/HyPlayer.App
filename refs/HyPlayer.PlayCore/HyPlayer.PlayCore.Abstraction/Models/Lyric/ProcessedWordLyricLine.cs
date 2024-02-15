namespace HyPlayer.PlayCore.Abstraction.Models.Lyric;

public class ProcessedWordLyricLine : ProcessedLyricLine
{
    public required List<LyricWord> Words { get; init; }
}