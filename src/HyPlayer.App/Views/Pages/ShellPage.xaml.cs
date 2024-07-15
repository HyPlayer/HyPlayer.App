using HyPlayer.Interfaces.Services;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Dialogs;
using HyPlayer.Views.Window;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using System;
using Windows.ApplicationModel;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;


namespace HyPlayer.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : ShellPageBase
    {

        public ShellPage()
        {
            InitializeComponent();

            App.contentFrame = contentFrame;
        }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var invokedItem = (args.InvokedItemContainer as NavigationViewItem)?.Tag?.ToString();
            if (invokedItem == null) return;
            var invokedItemTag = HyPlayer.App.GetService<IPageService>().GetPageType(invokedItem);
            var invokedItemName = (args.InvokedItemContainer as NavigationViewItem)?.Content?.ToString();


            HyPlayer.App.GetService<INavigationService>().NavigateTo(invokedItemTag);
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
