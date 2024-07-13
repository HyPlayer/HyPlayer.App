using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Lyric;

public interface ILyricFurigana : ILyricLine
{
    public string? Furigana { get; set; } // TODO: Need to define format of furigana
}