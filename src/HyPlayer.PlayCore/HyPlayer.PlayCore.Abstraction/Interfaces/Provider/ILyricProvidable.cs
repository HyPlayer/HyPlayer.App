using HyPlayer.PlayCore.Abstraction.Models.Lyric;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface ILyricProvidable : IProvider
{
    public Task<List<RawLyricInfo>> GetLyricInfoAsync(SingleSongBase song, CancellationToken ctk = new());
}