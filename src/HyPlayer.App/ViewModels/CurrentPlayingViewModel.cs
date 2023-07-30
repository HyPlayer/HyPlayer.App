using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using Depository.Abstraction.Interfaces;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;
using HyPlayer.PlayCore.Abstraction.Models.Songs;

namespace HyPlayer.App.ViewModels;

public partial class CurrentPlayingViewModel : ObservableObject, ISingletonViewModel, INotificationSubscriber<CurrentSongChangedNotification>
{

    [ObservableProperty] private SingleSongBase? _song;
    
    public Task HandleNotificationAsync(CurrentSongChangedNotification notification,
                                              CancellationToken ctk = new CancellationToken())
    {
        App.GetDispatcherQueue().TryEnqueue(() =>
        {
            Song = notification.CurrentPlayingSong;
        });
        return Task.CompletedTask;
    }
}