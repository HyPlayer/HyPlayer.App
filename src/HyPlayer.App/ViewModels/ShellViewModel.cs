using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.Views.Controls.Dialogs;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace HyPlayer.App.ViewModels
{
    public partial class ShellViewModel : ObservableObject, IScopedViewModel
    {
        public ShellViewModel() 
        { 

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

        [RelayCommand]
        public async Task OnUserButtonClickedAsync()
        {
            var signin_dialog = new SignInDialog();
            signin_dialog.XamlRoot = App.AppXamlRoot;
            var result = await signin_dialog.ShowAsync();
        }

        

        public bool CanBack => App.GetService<INavigationService>().CanGoBack;
    }
}
