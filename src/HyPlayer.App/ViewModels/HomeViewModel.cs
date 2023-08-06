using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.App.Interfaces;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.Extensions.Mappers;
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
    [ObservableProperty] private List<NeteaseSong>? _personalFM;

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
                (await ((await _provider.GetRecommendation("pl")) as NeteaseActionGettableContainer)?.GetAllItems())
                .Select(t => (NeteasePlaylist)t).ToList();

            // �Ƽ�����
            RecommendedSongs =
                (await ((await _provider.GetRecommendation("sg")) as NeteaseActionGettableContainer)?.GetAllItems())
                .Select(t => (NeteaseSong)t).ToList();
            
            // ˽���״�
            PersonalFM =
                (await _provider.RequestAsync(NeteaseApis.PersonalFmApi, new PersonalFmRequest())).Match(success => success.Items?.Select(
                                      t => (NeteaseSong)
                                          t.MapToNeteaseMusic()).ToList() ?? new List<NeteaseSong>(),
                                  error => throw error);
        }

        // ����¼����

        // ���а�
        TopLists =
            (await ((await _provider.GetRecommendation("ct")) as NeteaseActionGettableContainer)?.GetAllItems())
            .Select(t => (NeteasePlaylist)t).ToList();
    }
}