using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using HyPlayer.App.Views.Controls.Base;
using HyPlayer.App.Interfaces.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Controls.App
{
    public sealed partial class DesktopNavigationView : NavigationViewBase
    {
        public DesktopNavigationView()
        {
            this.InitializeComponent();
        }

        public Frame ContentFrame => contentFrame;

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var invokedItemTag = HyPlayer.App.App.GetService<IPageService>().GetPageType((args.InvokedItemContainer as NavigationViewItem)?.Tag?.ToString());
            var invokedItemName = (args.InvokedItemContainer as NavigationViewItem)?.Content?.ToString();


            HyPlayer.App.App.GetService<INavigationService>().NavigateTo(invokedItemTag);
            // NavView.Header = invokedItemName;        
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = Home;
        }
    }
}
