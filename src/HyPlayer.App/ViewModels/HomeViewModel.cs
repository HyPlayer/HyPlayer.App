using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;


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
            if((await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 推荐歌单
                PlayLists =
                    (await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteasePlaylist)t).ToList();
            }
            
            
            if((await ((await _provider.GetRecommendationAsync("sg")) as NeteaseActionGettableContainer)?.GetAllItemsAsync()) is not null)
            {
                // 推荐歌曲
                RecommendedSongs =
                    (await ((await _provider.GetRecommendationAsync("sg")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                    .Select(t => (NeteaseSong)t).ToList();
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
}
