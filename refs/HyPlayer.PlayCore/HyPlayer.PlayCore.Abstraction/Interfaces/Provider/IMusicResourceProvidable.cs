using HyPlayer.PlayCore.Abstraction.Models.Resources;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.Abstraction.Interfaces.Provider;

public interface IMusicResourceProvidable : IProvider
{
    public Task<MusicResourceBase?> GetMusicResourceAsync(SingleSongBase song, ResourceQualityTag qualityTag, CancellationToken ctk = new());
}