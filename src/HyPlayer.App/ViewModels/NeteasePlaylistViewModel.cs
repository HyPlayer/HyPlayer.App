using CommunityToolkit.Mvvm.ComponentModel;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.App.ViewModels;

public class NeteasePlaylistViewModel : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{
    public double? ScrollValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int? ConnectedItemIndex { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string? ConnectedElementName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private readonly NeteaseProvider.NeteaseProvider _provider;
    private readonly NeteasePlaylist _playList;
    

    public NeteasePlaylistViewModel(NeteaseProvider.NeteaseProvider provider, NeteasePlaylist playList = null)
    {
        _provider = provider;
        _playList = playList;
    }
}