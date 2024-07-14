using AsyncAwaitBestPractices;
using HyPlayer.Interfaces.Views;
using HyPlayer.NeteaseProvider.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using HomeViewModel = HyPlayer.ViewModels.HomeViewModel;

namespace HyPlayer.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : HomePageBase
    {
        public HomePage()
        {
            InitializeComponent();
        }

        [SuppressMessage("ReSharper", "ResourceItemNotResolved")]
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.GetSongsAsync().SafeFireAndForget();
            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;
            if (hour < 11 && hour >= 6)
            {
                Greetings.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_Morning").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_Morning").ValueAsString;
            }
            else if (hour >= 11 && hour < 13)
            {
                Greetings.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_Noon").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_Noon").ValueAsString;
            }
            else if (hour >= 13 && hour < 17)
            {
                Greetings.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_Afternoon").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_Noon").ValueAsString;
            }
            else if (hour >= 17 && hour < 23)
            {
                Greetings.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_Night").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_Night").ValueAsString;
            }
            else
            {
                Greetings.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_DeepNight").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_DeepNight").ValueAsString;
            }
        }


        private void OnPlaylistItemClicked(object sender, ItemClickEventArgs e)
        {
            // Debug.WriteLine($"Clicking on {(e.ClickedItem as ProvidableItemBase)?.Name}");

            App.GetService<INavigationService>().NavigateTo(typeof(PlaylistPage), (e.ClickedItem as NeteasePlaylist));
        }

        private void HomePageTopChart_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var playlist = button.CommandParameter as NeteasePlaylist;
            Debug.WriteLine($"Clicking on {playlist!.Name}");
            App.GetService<INavigationService>().NavigateTo(typeof(PlaylistPage), playlist);

        }
    }

    public class HomePageBase : AppPageWithScopedViewModelBase<HomeViewModel>
    {

    }
}