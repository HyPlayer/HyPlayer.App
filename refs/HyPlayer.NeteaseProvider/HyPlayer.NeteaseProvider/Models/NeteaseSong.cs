using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;
using System.Diagnostics.Contracts;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseSong : SingleSongBase, IHasTranslation, IHasCover
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.SingleSong;

    public string[]? Alias { get; set; }
    public bool HasCopyright { get; set; }
    public string? MvId { get; set; }
    public string? CdName { get; set; }
    public int TrackNumber { get; set; }
    public string? CoverUrl { get; set; }

    public List<PersonBase>? Artists { get; set; } = new() { };

    public override Task<List<PersonBase>?> GetCreatorsAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(Artists);
    }

    public string? Translation { get; set; }
    public Task<ImageResourceBase?> GetCoverAsync(ImageResourceQualityTag? qualityTag = null, CancellationToken ctk = new())
    {
        return Task.FromResult<ImageResourceBase?>(new NeteaseImageResource() { Url = CoverUrl, QualityTag = qualityTag });
    }
}