using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.ViewModels;


namespace HyPlayer.ViewModels
{
    public partial class ShellViewModel : ObservableObject, IScopedViewModel
    {
        private readonly INavigationViewService _navigationViewService;
        private readonly INavigationService _navigationService;

        [ObservableProperty] private AccountViewModel _accountViewModel;
        public bool IsProgressBarVisible => App.GetService<IShellService>().IsProgressBarVisible;

        public ShellViewModel(AccountViewModel accountViewModel, INavigationViewService navigationViewService, INavigationService navigationService)
        {
            _accountViewModel = accountViewModel;
            _navigationViewService = navigationViewService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void GoBack()
        {
            var canGoBack = App.GetService<INavigationService>().CanGoBack;
            if(canGoBack)
            {
                App.GetService<INavigationService>().GoBack();
            }
        }
       

        

        public bool CanBack => App.GetService<INavigationService>().CanGoBack;
    }
}
