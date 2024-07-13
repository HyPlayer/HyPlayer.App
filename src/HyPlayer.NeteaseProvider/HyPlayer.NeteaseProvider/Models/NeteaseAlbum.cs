using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseAlbum : AlbumBase, IHasCover, IHasTranslation, IHasDescription,IHasCreators
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.Album;
    public string? PictureUrl { get; set; }

    public List<string>? Alias { get; set; }
    public List<string>? Translations { get; set; }
    public string? Company { get; set; }
    public string? BriefDescription { get; set; }
    public string? SubType { get; set; }
    public string? AlbumType { get; set; }
    public bool IsSubscribed { get; set; }
    public List<NeteaseArtist>? Artists { get; set; }
    

    public async Task<ImageResourceBase?> GetCoverAsync(CancellationToken ctk = new())
    {
        return new NeteaseImageResource
               {
                   Url = PictureUrl,
                   HasContent = true
               };
    }

    public string? Translation { get; set; }
    public string? Description { get; set; }
    public Task<List<PersonBase>?> GetCreatorsAsync(CancellationToken ctk = new())
    {
        if (Artists is null) return Task.FromResult<List<PersonBase>?>(null);
        return Task.FromResult(Artists?.Select(ar=>(PersonBase)ar).ToList());
    }

    public List<string>? CreatorList { get; init; }
}