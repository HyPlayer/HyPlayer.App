using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Media.Animation;

using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.PlayListControllers;
using HyPlayer.NeteaseApi;
using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider;
//using HyPlayer.NeteaseProvider.Mappers;
using MapToNeteasePlaylist = HyPlayer.NeteaseProvider.Mappers.PlaylistItemToNeteasePlaylistMapper;
using ViewModels.App.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using Microsoft.UI.Xaml.Media;

namespace ViewModels.App;

public partial class HomeViewModel 
    : ObservableObject , IScrollableViewModel , IConnectedViewModel
{
    public int? ConnectedItemIndex { get; set; }
    public string ConnectedElementName { get ; set; }
    public double ScrollValue { get; set; }

    [ObservableProperty]
    private List<NeteaseSong> songs;
    [ObservableProperty]
    private List<NeteasePlaylist> playLists;
    [ObservableProperty]
    private List<NeteasePlaylist> leaderboards;
    [ObservableProperty]
    private List<string> imageList;


    [ObservableProperty]
    private string songName;
    [ObservableProperty]
    private string artist;

    private readonly NeteaseProvider _provider;

    public HomeViewModel()
    {
        
        _provider = new NeteaseProvider();
        _ = GetSongsAsync();
    }
    public HomeViewModel(string songName,string artist)
    {
        SongName = songName;
        Artist = artist;
        _ = GetSongsAsync();
    }
    [RelayCommand]
    public async Task GetSongsAsync()
    {
        // Songs = Song.CreateSampleList();
        // PlayLists = PlayList.CreateSampleList();
        // Leaderboards = PlayList.CreateSampleList();

        
        var result = await _provider.RequestAsync(NeteaseApis.ToplistApi, new ToplistRequest());
        result.Match(
           success => 
           Leaderboards = success.List?.Select(t => (NeteasePlaylist)t.MapToNeteasePlaylist()).ToList() ?? new List<NeteasePlaylist>(),
           error => { throw error; });
        var imgresult = await _provider.RequestAsync(NeteaseApis.ToplistApi, new ToplistRequest());
        imgresult.Match(
           success =>
           ImageList = (List<string>)(success.List?.Select(async t => await (await t.MapToNeteasePlaylist().GetCover())
           .GetResource(new ImageResourceQualityTag(), typeof(string)))),
           error => { throw error; });
        
    }
    [RelayCommand]
    public void ChangeSong()
    {
        SongName = "UpdatedSongName";
        Artist = "UpdatedArtist";
    }
    [RelayCommand]
    public void OpenSongList()
    {
        // NavigationService.Navigate(typeof(SongListPage),new SongListPageVM(),new ContinuumNavigationTransitionInfo());
    }

    [RelayCommand]
    public async Task<string> GetCoversAsync(NeteasePlaylist playlist) => (string) await (await playlist.GetCover()).GetResource(new ImageResourceQualityTag(), typeof(string));
    
}
