using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Lyric;

public interface ILyricSoramimi : ILyricLine
{
    public string? Soramimi { get; set; }
}