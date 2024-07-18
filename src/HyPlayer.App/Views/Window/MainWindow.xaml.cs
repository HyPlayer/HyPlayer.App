using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Pages;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WinRT.Interop;
using WinUIEx;
using AppWindow = Microsoft.UI.Windowing.AppWindow;
using AppWindowTitleBar = Microsoft.UI.Windowing.AppWindowTitleBar;

namespace HyPlayer.Views.Window
{
    public sealed partial class MainWindow : WindowEx
    {
        public static new MainWindow Current => _Current ?? new MainWindow();
        private static MainWindow _Current;

        private readonly IDepositoryResolveScope _scope;
        private readonly ShellViewModel _ShellViewModel;

        private bool CanGoBack => App.GetService<INavigationService>().CanGoBack;

        public MainWindow()
        {
            InitializeComponent();
            _scope = DepositoryResolveScope.Create();
            _ShellViewModel = App.GetDIContainer().ResolveInScope<ShellViewModel>(_scope);
        }

        private async void UserButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_ShellViewModel.AccountViewModel.IsLogin)
            {
                var signin_dialog = new SignInDialog();
                signin_dialog.XamlRoot = this.Content.XamlRoot;
                var result = await signin_dialog.ShowAsync();
            }
            else
            {
                App.GetService<INavigationService>().NavigateTo("UserPage", _ShellViewModel.AccountViewModel.User);
            }
        }

        private void AppTitleBar_BackRequested(Microsoft.UI.Xaml.Controls.TitleBar sender, object args)
        {
            App.GetService<INavigationService>().GoBack();
        }
    }
}
