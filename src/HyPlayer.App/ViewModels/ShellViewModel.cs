using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.ViewModels;

namespace HyPlayer.ViewModels
{
    public partial class ShellViewModel : ObservableObject, IScopedViewModel
    {
        [ObservableProperty] private AccountViewModel _accountViewModel;

        public ShellViewModel(AccountViewModel accountViewModel)
        {
            _accountViewModel = accountViewModel;
        }

        [RelayCommand]
        public void GoBack()
        {
            var canGoBack = App.GetService<INavigationService>().CanGoBack;
            if (canGoBack)
            {
                App.GetService<INavigationService>().GoBack();
            }
        }




        public bool CanBack => App.GetService<INavigationService>().CanGoBack;
    }
}
