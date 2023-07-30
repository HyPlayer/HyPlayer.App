using CommunityToolkit.Mvvm.ComponentModel;
using HyPlayer.App.Interfaces.ViewModels;

namespace HyPlayer.App.ViewModels;

public class SearchViewModel : ObservableObject, IViewModel
{
    private readonly NeteaseProvider.NeteaseProvider _provider;
    
    public SearchViewModel(NeteaseProvider.NeteaseProvider provider)
    {
        _provider = provider;
    }
    
}