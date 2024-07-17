using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Window;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using System;

using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Depository.Core;
using Depository.Extensions;
using WinUICommunity;
using Microsoft.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WinRT.Interop;
using Depository.Abstraction.Interfaces;



namespace HyPlayer.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : ShellPageBase
    {
        private AppWindow appWindow => MainWindow.Current.AppWindow;
        private Microsoft.UI.Xaml.Window currentWindow => MainWindow.Current;

        public ShellPage()
        {
            InitializeComponent();

            App.GetService<INavigationService>().RegisterFrameEvents(contentFrame);
        }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var invokedItem = (args.InvokedItemContainer as NavigationViewItem)?.Tag?.ToString();

            HyPlayer.App.GetService<INavigationService>().NavigateTo(invokedItem);
            // NavView.Header = invokedItemName;        
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = Home;
        }


        
    }

    public class ShellPageBase : AppPageWithScopedViewModelBase<ShellViewModel>
    {
        public ShellPageBase()
        {
        }
    }
}
