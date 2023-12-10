using Depository.Abstraction.Interfaces;
using Depository.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using AsyncAwaitBestPractices;
using Depository.Abstraction.Models.Options;
using Depository.Extensions;
using HomeViewModel = HyPlayer.ViewModels.HomeViewModel;
using App = HyPlayer.App;
using HyPlayer.Interfaces.Views;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.NeteaseProvider.Models;

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
            if (hour < 11 && hour>=6)
            {
                Greetings.Text=Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingPrefix_Morning").ValueAsString;
                GreetingsText.Text = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/HomePage_GreetingSuffix_Morning").ValueAsString;
            }
            else if(hour>=11 && hour<13)
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
    }

    public class HomePageBase : AppPageWithScopedViewModelBase<HomeViewModel>
    {

    }
}