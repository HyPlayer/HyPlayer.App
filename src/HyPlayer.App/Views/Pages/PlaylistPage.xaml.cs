using Microsoft.UI.Xaml.Navigation;
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.NeteaseProvider.Models;


namespace HyPlayer.Views.Pages
{
    
    public sealed partial class PlaylistPage : PlaylistPageBase
    {
        public PlaylistPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if((NeteasePlaylist)e.Parameter != null)
            {
                ViewModel.PlayList = (NeteasePlaylist?)e.Parameter;
                await ViewModel.GetSongsAsync();
            }

            
            //
        }
    }

    public class PlaylistPageBase : AppPageWithScopedViewModelBase<NeteasePlaylistViewModel>
    {
        
    }
}
