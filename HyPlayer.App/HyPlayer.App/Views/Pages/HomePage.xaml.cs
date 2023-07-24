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
using System.Threading.Tasks;
using ViewModels.App;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private Grid contentArea => ContentArea;
        public HomeViewModel ViewModel
        {
            get;
        }

        public HomePage()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<HomeViewModel>();
        }



        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            PlayListView.ItemClick -= ListItemClicked;
            LeaderboardView.ItemClick -= ListItemClicked;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await Task.Delay(1);
            PlayListView.ItemClick += ListItemClicked;
            LeaderboardView.ItemClick += ListItemClicked;
            
        }


        private void ListItemClicked(object sender, ItemClickEventArgs e)
        {
            // ConnectedAnimationHelper.PrepareForwardAnimation(ViewModel, (ListViewBase)sender, e.ClickedItem);
        }
        // protected override void OnPageUnloaded(object sender, RoutedEventArgs e) => Bindings.StopTracking();

    }
}
