using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace HyPlayer.Views.Controls.App
{
    public sealed partial class TitleBar : TitleBarBase
    {
        public TitleBar()
        {
            this.InitializeComponent();
        }

        private async void UserButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.AccountViewModel.IsLogin)
            {
                var signin_dialog = new SignInDialog();
                signin_dialog.XamlRoot = this.Content.XamlRoot;
                var result = await signin_dialog.ShowAsync();
            }
            else
            {
                HyPlayer.App.GetService<INavigationService>().NavigateTo(typeof(UserPage), ViewModel.AccountViewModel.User);
            }
        }
    }

    public class TitleBarBase : ReactiveControlBase<ShellViewModel>
    {

    }
}
