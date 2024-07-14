using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Lyric;

public interface ILyricRomaji : ILyricLine
{
    public string? Romaji { get; set; }
}