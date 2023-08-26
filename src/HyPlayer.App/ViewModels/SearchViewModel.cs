using CommunityToolkit.Mvvm.ComponentModel;
using HyPlayer.Interfaces.ViewModels;

namespace HyPlayer.ViewModels;

public class SearchViewModel : ObservableObject, IViewModel, IScopedViewModel
{
    private readonly NeteaseProvider.NeteaseProvider _provider;
    
    public SearchViewModel(NeteaseProvider.NeteaseProvider provider)
    {
        _provider = provider;
    }
    
}