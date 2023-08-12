using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HyPlayer.App.ViewModels;

public partial class NeteasePlaylistViewModel : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{
    public double? ScrollValue { get; set ; }
    public int? ConnectedItemIndex { get ; set; }
    public string? ConnectedElementName { get; set; }

    private readonly NeteaseProvider.NeteaseProvider _provider;
    private readonly NeteasePlaylist? _playList;

    public string Title => _playList.Name;
    public string Description => _playList.Description;
    public long PlayCount => _playList.PlayCount;
    public long ShareCount => _playList.ShareCount;

    public List<NeteaseSong>? SongsList;


    public NeteasePlaylistViewModel(NeteaseProvider.NeteaseProvider provider, NeteasePlaylist playList = null)
    {
        _provider = provider;
        _playList = playList;
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        //SongsList = await _playList.GetAllItemsAsync();
    }
}