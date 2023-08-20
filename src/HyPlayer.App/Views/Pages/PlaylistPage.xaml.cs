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
using HyPlayer.App.Interfaces.Views;
using HyPlayer.App.ViewModels;
using System.Threading.Tasks;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction;
using AsyncAwaitBestPractices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Pages
{
    
    public sealed partial class PlaylistPage : PlaylistPageBase
    {
        public PlaylistPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if((NeteasePlaylist)e.Parameter != null)
            {
                ViewModel.PlayList = (NeteasePlaylist?)e.Parameter;
            }

            ViewModel.GetSongsAsync();
            //.SafeFireAndForget()
        }
    }

    public class PlaylistPageBase : AppPageWithScopedViewModelBase<NeteasePlaylistViewModel>
    {
        
    }
}
