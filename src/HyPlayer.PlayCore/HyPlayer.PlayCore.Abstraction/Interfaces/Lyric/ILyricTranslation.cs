using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Lyric;

public interface ILyricTranslation : ILyricLine
{
    public string? Translation { get; set; }
}