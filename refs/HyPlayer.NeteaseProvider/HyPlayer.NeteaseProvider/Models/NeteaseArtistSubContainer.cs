using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseArtistSubContainer : LinerContainerBase, IProgressiveLoadingContainer
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.Artist;

    public override async Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new())
    {
        var itemType = ActualId.Substring(0, 3);
        var artistId = ActualId.Substring(3);
        switch (itemType)
        {
            case "hot":
            default:
                var result = await NeteaseProvider.Instance.RequestAsync(
                    NeteaseApis.ArtistSongsApi,
                    new ArtistSongsRequest
                    {
                        ArtistId = artistId,
                        OrderType = "hot",
                        Offset = 0,
                        Limit = 50
                    }, ctk).ConfigureAwait(false);
                return result.Match(
                    success => 
                        success.Songs?.Select(song => (ProvidableItemBase)song.MapToNeteaseMusic()).ToList(),
                    error => new List<ProvidableItemBase>()
                )?? new List<ProvidableItemBase>();
            case "tim":
                var resTime = await NeteaseProvider.Instance.RequestAsync(
                    NeteaseApis.ArtistSongsApi,
                    new ArtistSongsRequest
                    {
                        ArtistId = artistId,
                        OrderType = "time",
                        Offset = 0,
                        Limit = 50
                    }, ctk).ConfigureAwait(false);
                return resTime.Match(
                    success =>
                        success.Songs?.Select(song => (ProvidableItemBase)song.MapToNeteaseMusic()).ToList(),
                    error => new List<ProvidableItemBase>()
                ) ?? new List<ProvidableItemBase>();
            case "alb":
                var resAlbum =
                    await NeteaseProvider.Instance.RequestAsync(
                        NeteaseApis.ArtistAlbumsApi,
                        new ArtistAlbumsRequest()
                        {
                            ArtistId = artistId,
                            Limit = 50,
                            Start = 0
                        }, ctk).ConfigureAwait(false);
                return resAlbum.Match(
                    success => 
                        success.Albums?.Select(alb => (ProvidableItemBase)alb.MapToNeteaseAlbum()!).ToList() ?? new(),
                    error => new List<ProvidableItemBase>()
                );
        }
    }

    public async Task<(bool, List<ProvidableItemBase>)> GetProgressiveItemsListAsync(int start = 0, int count = 50,CancellationToken ctk = new())
    {
        var itemType = ActualId.Substring(0, 3);
        var artistId = ActualId.Substring(3);
        switch (itemType)
        {
            case "hot":
            default:
                var result = await NeteaseProvider.Instance.RequestAsync(
                    NeteaseApis.ArtistSongsApi,
                    new ArtistSongsRequest
                    {
                        ArtistId = artistId,
                        OrderType = "hot",
                        Offset = start,
                        Limit = count
                    }).ConfigureAwait(false);
                return result.Match(
                    success => (success.HasMore,
                        success.Songs?.Select(song => (ProvidableItemBase)song.MapToNeteaseMusic()).ToList() ?? new List<ProvidableItemBase>()),
                    error => (false,new List<ProvidableItemBase>())
                );
            case "tim":
                var resTime = await NeteaseProvider.Instance.RequestAsync(
                    NeteaseApis.ArtistSongsApi,
                    new ArtistSongsRequest
                    {
                        ArtistId = artistId,
                        OrderType = "time",
                        Offset = 0,
                        Limit = 50
                    }, ctk).ConfigureAwait(false);
                return resTime.Match(
                    success => (success.HasMore,
                                    success.Songs?.Select(song => (ProvidableItemBase)song.MapToNeteaseMusic()).ToList()?? new List<ProvidableItemBase>()),
                                error => (false,new List<ProvidableItemBase>())
                );

        }
    }

    public int MaxProgressiveCount => 50;
}