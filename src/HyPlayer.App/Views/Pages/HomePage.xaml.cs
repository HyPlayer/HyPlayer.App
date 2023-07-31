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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Depository.Abstraction.Models.Options;
using Depository.Extensions;
using HomeViewModel = HyPlayer.App.ViewModels.HomeViewModel;
using AsyncAwaitBestPractices;
using HyPlayer.App.Interfaces.Views;
using HyPlayer.PlayCore.Abstraction.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Pages
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.GetSongsAsync().SafeFireAndForget();
        }


        private void OnPlaylistItemClicked(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine($"Clicking on {(e.ClickedItem as ProvidableItemBase)?.Name}");
        }
    }

    public class HomePageBase : AppPageWithScopedViewModelBase<HomeViewModel>
    {

    }
}