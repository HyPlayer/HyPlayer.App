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
        // ���ڵ�¼�����
        if (AccountViewModel.IsLogin)
        {
            // �Ƽ��赥
            PlayLists =
                (await ((await _provider.GetRecommendationAsync("pl")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                .Select(t => (NeteasePlaylist)t).ToList();

            // �Ƽ�����
            RecommendedSongs =
                (await ((await _provider.GetRecommendationAsync("sg")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
                .Select(t => (NeteaseSong)t).ToList();
        }

        // ����¼����

        // ���а�
        TopLists =
            (await ((await _provider.GetRecommendationAsync("ct")) as NeteaseActionGettableContainer)?.GetAllItemsAsync())
            .Select(t => (NeteasePlaylist)t).ToList();
    }
}