using Depository.Abstraction.Interfaces;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;

namespace HyPlayer.PlayCore.PlayListControllers;

public class DefaultPlayListManager : PlayListManagerBase
{
    private readonly List<SingleSongBase> _list = new();
    private readonly Dictionary<ContainerBase, List<SingleSongBase>> _containerSongsDictionary = new();
    private readonly List<ContainerBase> _currentSongListContainers = new();

    private readonly IDepository _depository;
    private readonly INotificationHub _notificationHub;

    public DefaultPlayListManager(IDepository depository, INotificationHub notificationHub)
    {
        _depository = depository;
        _notificationHub = notificationHub;
    }

    public override async Task AddSongContainerAsync(ContainerBase container, CancellationToken ctk = new())
    {
        _currentSongListContainers.RemoveAll(t => t is UndeterminedContainerBase);
        if (container is UndeterminedContainerBase)
        {
            _currentSongListContainers.Clear();
        }

        _currentSongListContainers.Add(container);
        var songsList = new List<SingleSongBase>();
        await AppendSongContainerToList(container, songsList, ctk);
        _containerSongsDictionary[container] = songsList;
    }

    private async Task AppendSongContainerToList(
        ContainerBase container,
        List<SingleSongBase> list,
        CancellationToken ctk = new())
    {
        if (container is IProgressiveLoadingContainer progressiveLoadingContainer)
        {
            var hasMore = true;
            var start = 0;
            while (hasMore)
            {
                (hasMore, var progressiveList) = await progressiveLoadingContainer.GetProgressiveItemsListAsync(
                    start, progressiveLoadingContainer.MaxProgressiveCount,
                    ctk);
                var resultList = progressiveList.OfType<SingleSongBase>().ToList();
                list.AddRange(resultList);
                _list.AddRange(resultList);
                await _notificationHub.PublishNotificationAsync(new InnerPlayListProgressiveAddedNotification
                                                                {
                                                                    AddedSongs = resultList
                                                                }, ctk);
            }

            await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
            return;
        }

        if (container is LinerContainerBase linerContainer)
        {
            var resultList = (await linerContainer.GetAllItemsAsync(ctk)).OfType<SingleSongBase>().ToList();
            list.AddRange(resultList);
            _list.AddRange(resultList);
            await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
            return;
        }

        if (container is UndeterminedContainerBase undeterminedContainer)
        {
            var resultList = (await undeterminedContainer.GetNextItemsRangeAsync(ctk)).OfType<SingleSongBase>()
                .ToList();
            list.AddRange(resultList);
            _list.AddRange(resultList);
            await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
            return;
        }
    }


    public override async Task RemoveSongContainerAsync(ContainerBase container, CancellationToken ctk = new())
    {
        _currentSongListContainers.Remove(container);
        foreach (var singleSongBase in _containerSongsDictionary[container])
        {
            _list.Remove(singleSongBase);
        }

        _containerSongsDictionary[container].Clear();
        _containerSongsDictionary.Remove(container);
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task ClearSongContainersAsync(CancellationToken ctk = new())
    {
        _currentSongListContainers.Clear();
        foreach (var singleSongBases in _containerSongsDictionary.Values)
        {
            foreach (var singleSongBase in singleSongBases)
            {
                _list.Remove(singleSongBase);
            }

            singleSongBases.Clear();
        }

        _list.Clear();
        _containerSongsDictionary.Clear();
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override Task<List<ContainerBase>> GetAllSongContainersAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(_currentSongListContainers);
    }

    public override Task<List<SingleSongBase>> GetPlayListAsync(CancellationToken ctk = new())
    {
        return Task.FromResult(_list);
    }

    public override async Task AddSongAsync(SingleSongBase song, int index = -1, CancellationToken ctk = new())
    {
        if (index != -1)
            _list.Insert(index, song);
        else
            _list.Add(song);
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task AddSongRangeAsync(
        List<SingleSongBase> songs,
        int index = -1,
        CancellationToken ctk = new())
    {
        if (index != -1)
            _list.InsertRange(index, songs);
        else
            _list.AddRange(songs);
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task RemoveSongAsync(SingleSongBase song, CancellationToken ctk = new())
    {
        _list.Remove(song);
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task RemoveSongRangeAsync(List<SingleSongBase> songs, CancellationToken ctk = new())
    {
        foreach (var singleSongBase in songs)
        {
            _list.Remove(singleSongBase);
        }

        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task ClearSongsAsync(CancellationToken ctk = new())
    {
        _list.Clear();
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }

    public override async Task SetSongListAsync(List<SingleSongBase> songs, CancellationToken ctk = new())
    {
        _list.Clear();
        _list.AddRange(songs);
        await _notificationHub.PublishNotificationAsync(new InnerPlayListChangedNotification(), ctk);
    }
}