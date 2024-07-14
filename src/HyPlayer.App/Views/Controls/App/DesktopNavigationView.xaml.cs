using HyPlayer.Interfaces.Views;
using HyPlayer.Views.Controls.Base;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

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
}
