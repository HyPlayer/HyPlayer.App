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
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using HyPlayer.NeteaseApi.Models.ResponseModels;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.Views.Pages;
using Microsoft.UI.Xaml.Controls;

namespace HyPlayer.App.ViewModels;

public partial class HomeViewModel
    : ObservableObject, IScrollableViewModel, IConnectedViewModel, IScopedViewModel
{

    public int? ConnectedItemIndex { get; set; }
    public string? ConnectedElementName { get; set; }
    public double? ScrollValue { get; set; }

    [ObservableProperty] private AccountViewModel _accountViewModel;

    [ObservableProperty] private List<NeteaseSong>? _recommendedSongs;
    [ObservableProperty] private List<NeteasePlaylist>? _playLists;
    [ObservableProperty] private List<NeteasePlaylist>? _topLists;
    [ObservableProperty] private NeteasePlaylist? _official;

    [ObservableProperty] private CurrentPlayingViewModel _currentPlaying;

    private readonly NeteaseProvider.NeteaseProvider _provider;

    public HomeViewModel(NeteaseProvider.NeteaseProvider provider, CurrentPlayingViewModel currentPlaying, AccountViewModel accountViewModel)
    {
        _provider = provider;
        CurrentPlaying = currentPlaying;
        AccountViewModel = accountViewModel;
    }

    [RelayCommand]
    public async Task GetSongsAsync()
    {
        // 仅在登录后加载
        if (AccountViewModel.IsLogin)
        {
            /*
            if((await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 推荐歌单
                PlayLists =
                    (await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteasePlaylist)t).ToList();
            }
            */
            
            if((await ((await _provider.GetRecommendationAsync("sg")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 推荐歌曲
                RecommendedSongs =
                    (await ((await _provider.GetRecommendationAsync("sg")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteaseSong)t).ToList();
            }

            if ((await ((await _provider.GetRecommendationAsync("PC")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 官方歌单
                Official =
                    (await ((await _provider.GetRecommendationAsync("PC")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteasePlaylist)t).ToList().FirstOrDefault();
            }
        }

        // 不登录加载

        // 排行榜
        if((await ((await _provider.GetRecommendationAsync("ct")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
        {
            TopLists =
                (await ((await _provider?.GetRecommendationAsync("ct")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())?
                .Select(t => (NeteasePlaylist)t).ToList();
        }
        
    }

    [RelayCommand]
    public async Task GetPlayListAsync()
    {
        if (AccountViewModel.IsLogin)
        {
            if ((await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 推荐歌单
                PlayLists =
                    (await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteasePlaylist)t).ToList();
            }
        }
    }

    public void OnPlaylistItemClicked(object sender, ItemClickEventArgs e)
    {
        // Debug.WriteLine($"Clicking on {(e.ClickedItem as ProvidableItemBase)?.Name}");

        App.GetService<INavigationService>().NavigateTo(typeof(PlaylistPage), (e.ClickedItem as NeteasePlaylist));
    }
}