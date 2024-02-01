using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
            HyPlayer.App.GetService<INavigationViewService>().Initialize(NavView);
        }
        

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = Home;
        }
    }
}
