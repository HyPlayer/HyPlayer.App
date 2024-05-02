using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels;

public partial class NeteasePlaylistViewModel : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{
    public double? ScrollValue { get; set ; }
    public int? ConnectedItemIndex { get ; set; }
    public string? ConnectedElementName { get; set; }

    private readonly NeteaseProvider.NeteaseProvider _provider;
    [ObservableProperty]
    public NeteasePlaylist? _playList;

    public string? Title => PlayList?.Name;
    public string? Description => PlayList?.Description;
    public long? PlayCount => PlayList?.PlayCount;
    public long? ShareCount => PlayList?.ShareCount;
    [ObservableProperty]
    public ObservableCollection<NeteaseSong>? _songsList;


    public NeteasePlaylistViewModel(NeteaseProvider.NeteaseProvider provider)
    {
        _provider = provider;   
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        if(PlayList is not null)
        {
            SongsList = new ObservableCollection<NeteaseSong>((await PlayList.GetAllItemsAsync())?.Select(t => (NeteaseSong)t));
        }
    }

    [RelayCommand]
    public async Task SubscribePlaylistAsync()
    {
        if (PlayList.Subscribed==false)
        {
            await _provider.LikeProvidableItemAsync($"pl{PlayList.ActualId}", null);
        }
        else
        {
            await _provider.UnlikeProvidableItemAsync($"pl{PlayList.ActualId}", null);
        }
        OnPropertyChanged(nameof(PlayList));
    }
}