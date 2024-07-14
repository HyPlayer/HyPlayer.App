using CommunityToolkit.Mvvm.ComponentModel;
using Depository.Abstraction.Interfaces.NotificationHub;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;
using System.Threading;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels;

public partial class CurrentPlayingViewModel : ObservableObject, ISingletonViewModel, INotificationSubscriber<CurrentSongChangedNotification>
{

    [ObservableProperty] private SingleSongBase? _song;

    public Task HandleNotificationAsync(CurrentSongChangedNotification notification,
                                              CancellationToken ctk = new CancellationToken())
    {
        App.GetDispatcherQueue()?.TryEnqueue(() =>
        {
            Song = notification.CurrentPlayingSong;
        });
        return Task.CompletedTask;
    }
}