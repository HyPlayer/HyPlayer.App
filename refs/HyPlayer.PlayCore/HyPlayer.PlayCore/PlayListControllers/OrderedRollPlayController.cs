using AsyncAwaitBestPractices;
using Depository.Abstraction.Interfaces;
using Depository.Abstraction.Interfaces.NotificationHub;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListController;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.PlayListControllers;

public class OrderedRollPlayController : PlayControllerBase,
                                         IReversiblePlayListController,
                                         INavigateSongPlayListController,
                                         IIndexedPlayListController,
                                         IPlayListGettablePlaylistController,
                                         INotifyDependencyChanged<PlayListManagerBase>,
                                         INotificationSubscriber<InnerPlayListChangedNotification>
{
    private int _index = 0;
    private readonly IDepository _depository;
    private readonly INotificationHub _notificationHub;
    private PlayListManagerBase? _playListManager;
    private List<SingleSongBase> _list = new();

    public OrderedRollPlayController(IDepository depository, PlayListManagerBase? playListManager, INotificationHub notificationHub)
    {
        _depository = depository;
        _playListManager = playListManager;
        _notificationHub = notificationHub;
    }


    public override Task<SingleSongBase?> MoveNextAsync(CancellationToken ctk = new())
    {
        if (_list.Count == 0) return Task.FromResult<SingleSongBase?>(null);
        if (_list.Count <= _index + 1)
            _index = -1;
        _index++;
        var curSong = _list[_index];
        _notificationHub.PublishNotificationAsync(new CurrentSongChangedNotification() { CurrentPlayingSong = curSong },
                                                  ctk).SafeFireAndForget();
        return Task.FromResult<SingleSongBase?>(curSong);
    }

    public override Task<SingleSongBase?> MovePreviousAsync(CancellationToken ctk = new())
    {
        if (_list.Count == 0) return Task.FromResult<SingleSongBase?>(null);
        if (_index <= 0)
            _index = _list.Count;
        _index--;
        var curSong = _list[_index];
        _notificationHub.PublishNotificationAsync(new CurrentSongChangedNotification() { CurrentPlayingSong = curSong },
                                                  ctk).SafeFireAndForget();
        return Task.FromResult<SingleSongBase?>(curSong);
    }

    public override Task<SingleSongBase?> MoveToIndexAsync(int index, CancellationToken ctk = new())
    {
        if (_list.Count >= index || index < 0) return Task.FromResult<SingleSongBase?>(null);
        _index = index;
        var curSong = _list[index];
        _notificationHub.PublishNotificationAsync(new CurrentSongChangedNotification() { CurrentPlayingSong = curSong },
                                                  ctk).SafeFireAndForget();
        return Task.FromResult<SingleSongBase?>(_list[index]);
    }

    public Task Reverse(CancellationToken ctk = new())
    {
        _list.Reverse();
        _index = _list.Count - _index - 1;
        _notificationHub.PublishNotificationAsync(new OrderedPlaylistChangedNotification() { IsRandom = false, OrderedList = _list }, ctk).SafeFireAndForget();
        return Task.CompletedTask;
    }

    public Task NavigateSongToAsync(SingleSongBase song, CancellationToken ctk = new())
    {
        var indexOfSong = _list.IndexOf(song);
        if (indexOfSong >= 0)
            _index = indexOfSong;
        _notificationHub.PublishNotificationAsync(new CurrentSongChangedNotification() { CurrentPlayingSong = _list[_index] },
                                                  ctk).SafeFireAndForget();
        return Task.CompletedTask;
    }

    public Task<int> GetCurrentIndexAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(_index);
    }

    public Task<SingleSongBase?> GetSongAtAsync(int index, CancellationToken ctk = new())
    {
        if (_list.Count >= index || index < 0) return Task.FromResult<SingleSongBase?>(null);
        return Task.FromResult<SingleSongBase?>(_list[index]);
    }

    public Task<List<SingleSongBase>> GetOrderedPlayListAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(_list);
    }

    public async void OnDependencyChanged(PlayListManagerBase? alwaysNullMarker)
    {
        _playListManager = _depository.ResolveDependency(typeof(PlayListManagerBase)) as PlayListManagerBase;
        _list = await (_playListManager?.GetPlayListAsync() ?? Task.FromResult(new List<SingleSongBase>()));
        _notificationHub.PublishNotificationAsync(new OrderedPlaylistChangedNotification() { IsRandom = false, OrderedList = _list }).SafeFireAndForget();
    }

    public async Task HandleNotificationAsync(
        InnerPlayListChangedNotification notification,
        CancellationToken ctk = new())
    {
        _list = await (_playListManager?.GetPlayListAsync(ctk) ?? Task.FromResult(new List<SingleSongBase>()));
        _notificationHub.PublishNotificationAsync(new OrderedPlaylistChangedNotification() { IsRandom = false, OrderedList = _list }, ctk).SafeFireAndForget();
    }
}