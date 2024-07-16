using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels;

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
            var recommendPlayLists = await _provider.GetRecommendationAsync("pl") as NeteaseActionGettableContainer;
            if (recommendPlayLists != null)
            {
                if (await recommendPlayLists.GetAllItemsAsync() is List<ProvidableItemBase> providableItems)
                {
                    // 推荐歌单
                    PlayLists = providableItems.Select(t => (NeteasePlaylist)t).ToList();

                }
            }


            var recommendedSongs = await _provider.GetRecommendationAsync("sg") as NeteaseActionGettableContainer;
            if (recommendedSongs != null)
            {
                if (await recommendedSongs.GetAllItemsAsync() is List<ProvidableItemBase> providableItems)
                {
                    // 推荐歌曲
                    RecommendedSongs = providableItems.Select(t => (NeteaseSong)t).ToList();
                }
            }


        }

        // 不登录加载

        // 排行榜
        var topLists = await _provider.GetRecommendationAsync("ct") as NeteaseActionGettableContainer;
        if (topLists != null)
        {
            if (await topLists.GetAllItemsAsync() is List<ProvidableItemBase> providableItems)
            {
                TopLists = providableItems.Select(t => (NeteasePlaylist)t).ToList();
            }
        }
    }
}
