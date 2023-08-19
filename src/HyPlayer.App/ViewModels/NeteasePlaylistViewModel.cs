using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.SingleItems;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyPlayer.App.ViewModels;

public partial class NeteasePlaylistViewModel : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{
    public double? ScrollValue { get; set ; }
    public int? ConnectedItemIndex { get ; set; }
    public string? ConnectedElementName { get; set; }

    private readonly NeteaseProvider.NeteaseProvider _provider;
    public NeteasePlaylist? _playList;

    public string Title => _playList.Name;
    public string Description => _playList.Description;
    public long PlayCount => _playList.PlayCount;
    public long ShareCount => _playList.ShareCount;
    [ObservableProperty]
    public List<SingleSongBase>? _songsList;


    public NeteasePlaylistViewModel(NeteaseProvider.NeteaseProvider provider)
    {
        _provider = provider;   
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        if(_playList is not null)
        {
            SongsList = (await _playList?.GetAllItemsAsync()).OfType<SingleSongBase>().ToList();
        }
        
    }
}