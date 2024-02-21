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
using HyPlayer.Views.Controls.Base;
using HyPlayer.Interfaces.Services;

namespace HyPlayer.Views.Controls.App
{
    public sealed partial class DesktopNavigationView : NavigationViewBase
    {
        public static readonly DependencyProperty IsPaneOpenProperty = DependencyProperty.Register(
            nameof(IsPaneOpen), typeof(bool), typeof(DesktopNavigationView), new PropertyMetadata(default(bool)));

        public bool IsPaneOpen
        {
            get => (bool)GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        public DesktopNavigationView()
        {
            this.InitializeComponent();
            HyPlayer.App.contentFrame = contentFrame;
        }

        

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var invokedItemTag = HyPlayer.App.GetService<IPageService>().GetPageType((args.InvokedItemContainer as NavigationViewItem)?.Tag?.ToString());
            var invokedItemName = (args.InvokedItemContainer as NavigationViewItem)?.Content?.ToString();


            HyPlayer.App.GetService<INavigationService>().NavigateTo(invokedItemTag);
            // NavView.Header = invokedItemName;        
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = Home;
        }
    }
}
