using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.App.Interfaces;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseApi;
using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction;
using HyPlayer.PlayCore.Abstraction.Models.Resources;

//using HyPlayer.NeteaseProvider.Mappers;

namespace HyPlayer.App.ViewModels;

public partial class HomeViewModel
    : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{
    public string UsrName { get 
        {
            if (_provider.LoginedUser != null)
            {
                return _provider.LoginedUser.Name;
            }
            else return "";
        } }
    public int? ConnectedItemIndex { get; set; }
    public string? ConnectedElementName { get; set; }
    public double? ScrollValue { get; set; }
    public bool IsSignIn
    {
        get
        {
            if (_provider.LoginedUser != null)
            {
                return true;
            }
            else return false; }
        }

    [ObservableProperty] private List<NeteaseSong>? _recommendedSongs;
    [ObservableProperty] private List<NeteasePlaylist>? _playLists;
    [ObservableProperty] private List<NeteasePlaylist>? _topLists;
    [ObservableProperty] private CurrentPlayingViewModel _currentPlaying;

    private readonly NeteaseProvider.NeteaseProvider _provider;

    public HomeViewModel(NeteaseProvider.NeteaseProvider provider,CurrentPlayingViewModel currentPlaying)
    {
        _provider = provider;
        _currentPlaying = currentPlaying;
        // GetSongsAsync().SafeFireAndForget();
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        // Songs = Song.CreateSampleList();
        // PlayLists = PlayList.CreateSampleList();
        // Leaderboards = PlayList.CreateSampleList();

        // ÍÆ¼ö¸èµ¥
        if (_provider.LoginedUser != null)
        {
            /*
            RecommendPlaylistsApi recommendPlaylistsApi = new RecommendPlaylistsApi();
            var recommend_playlists_result = await _provider.RequestAsync(NeteaseApis.RecommendPlaylistsApi, recommendPlaylistsApi);
            recommend_playlists_result.Match<List<NeteasePlaylist>>(
                success =>
                    PlayLists = success.List?.Select(t => t.MapToNeteasePlaylist()).ToList() ??
                                   new List<NeteasePlaylist>(),
                error => throw error);

            var recommend_song_result = await _provider.RequestAsync(NeteaseApis.ToplistApi, new ToplistRequest());
            recommend_song_result.Match<List<NeteasePlaylist>>(
                success =>
                    TopLists = success.List?.Select(t => t.MapToNeteasePlaylist()).ToList() ??
                                   new List<NeteasePlaylist>(),
                error => throw error);
            */
        }
        
        // ÍÆ¼ö¸èÇú
        /*
       */
        // ÅÅÐÐ°ñ
        var toplist_result = await _provider.RequestAsync(NeteaseApis.ToplistApi, new ToplistRequest());
        toplist_result.Match<List<NeteasePlaylist>>(
            success =>
                TopLists = success.List?.Select(t => t.MapToNeteasePlaylist()).ToList() ??
                               new List<NeteasePlaylist>(),
            error => throw error);
    }
}