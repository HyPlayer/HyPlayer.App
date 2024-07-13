using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;
using HyPlayer.PlayCore.Abstraction.Interfaces.ProvidableItem;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteasePlaylist : LinerContainerBase, IProgressiveLoadingContainer, IHasCover, IHasDescription,
                               IHasCreators
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.Playlist;

    private PlaylistTracksGetResponse.PlaylistWithTracksInfoDto.TrackIdItem[]? _trackIds;
    public string? CoverUrl;
    public NeteaseUser? Creator;
    public bool Subscribed { get; set; }
    public long UpdateTime { get; set; }
    public int TrackCount { get; set; }
    public long PlayCount { get; set; }
    public long SubscribedCount { get; set; }
    public long CommentCount { get; set; }
    public long ShareCount { get; set; }
    public bool IsNewImported { get; set; }

    public async Task UpdatePlaylistInfoAsync(CancellationToken ctk = new())
    {
        var results = await NeteaseProvider.Instance.RequestAsync(
            NeteaseApis.PlaylistDetailApi,
            new PlaylistDetailRequest
            {
                Id = ActualId
            }, ctk);
        results.Match(
            success =>
            {
                CoverUrl = success.Playlists?[0].CoverUrl;
                Description = success.Playlists?[0].Description;
                if (!string.IsNullOrEmpty(success.Playlists?[0].Name))
                    Name = success.Playlists?[0].Name!;
                Creator = success.Playlists?[0].Creator?.MapToNeteaseUser();
                CreatorList?.Clear();
                CreatorList?.Add(Creator?.Name!);
                Subscribed = success.Playlists?[0].Subscribed is true;
                UpdateTime = success.Playlists?[0].UpdateTime ?? 0;
                TrackCount = success.Playlists?[0].TrackCount ?? 0;
                PlayCount = success.Playlists?[0].PlayCount ?? 0;
                SubscribedCount = success.Playlists?[0].SubscribedCount ?? 0;
                CommentCount = success.Playlists?[0].CommentCount ?? 0;
                ShareCount = success.Playlists?[0].ShareCount ?? 0;
                IsNewImported = success.Playlists?[0].IsNewImported ?? false;

                return true;
            }, error => false);
    }

    public async Task UpdateTrackListAsync(CancellationToken ctk = new())
    {
        _trackIds = (await NeteaseProvider.Instance.RequestAsync(NeteaseApis.PlaylistTracksGetApi,
                                                                 new PlaylistTracksGetRequest()
                                                                 {
                                                                     Id = ActualId
                                                                 }, ctk)).Match(
            success => success.Playlist.TrackIds,
            error => { return Array.Empty<PlaylistTracksGetResponse.PlaylistWithTracksInfoDto.TrackIdItem>(); });
    }

    public override async Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new())
    {
        if (_trackIds is null)
            await UpdateTrackListAsync(ctk);
        if (_trackIds is { Length: > 0 })
            return (await NeteaseProvider.Instance.GetSingleSongRangeByIds(_trackIds.Select(t => t.Id).ToList(), ctk))
                   .Select(t => (ProvidableItemBase)t).ToList();
        return new List<ProvidableItemBase>();
    }

    public async Task<(bool, List<ProvidableItemBase>)> GetProgressiveItemsListAsync(
        int start, int count, CancellationToken ctk = new())
    {
        if (_trackIds is null)
            await UpdatePlaylistInfoAsync(ctk);
        if (_trackIds is not null)
            return (start + count < _trackIds.Length,
                    (await NeteaseProvider.Instance.GetSingleSongRangeByIds(
                        _trackIds.Skip(start).Take(count).Select(t => t.Id).ToList(), ctk))
                    .Select(t => (ProvidableItemBase)t).ToList());
        return (false, new List<ProvidableItemBase>());
    }

    public int MaxProgressiveCount => 200;

    public Task<ImageResourceBase?> GetCoverAsync(CancellationToken ctk = new())
    {
        return Task.FromResult<ImageResourceBase?>(
            new NeteaseImageResource
            {
                Url = CoverUrl,
            });
    }

    public string? Description { get; set; }

    public Task<List<PersonBase>?> GetCreatorsAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(new List<PersonBase> { Creator! })!;
    }

    public List<string>? CreatorList { get; init; } = new();
}