using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using System.Collections.Generic;
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
    public List<NeteaseSong>? _songsList;


    public NeteasePlaylistViewModel(NeteaseProvider.NeteaseProvider provider)
    {
        _provider = provider;   
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        if(PlayList is not null)
        {
            SongsList = (await PlayList.GetAllItemsAsync())?.Select(t => (NeteaseSong)t).ToList();
        }
        
    }
}