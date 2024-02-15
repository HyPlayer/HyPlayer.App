using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Lyric;

public interface ILyricProcessor : ILyricLine
{
    public Task<List<ProcessedLyricLine>> ProcessLyric(string lrcText, CancellationToken ctk = new());
}