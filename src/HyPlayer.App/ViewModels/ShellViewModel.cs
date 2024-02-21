using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Extensions.Helpers;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.Interfaces.Services;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels
{
    public partial class ShellViewModel : ObservableObject, IScopedViewModel
    {
        [ObservableProperty] private AccountViewModel _accountViewModel;
        [ObservableProperty] private String _title;

        public ShellViewModel(AccountViewModel accountViewModel) 
        { 
            _accountViewModel = accountViewModel;
            _title = WindowHelper.CurrentWindow.Title;
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
