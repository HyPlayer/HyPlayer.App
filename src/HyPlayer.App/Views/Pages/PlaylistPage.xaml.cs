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
using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using System.Threading.Tasks;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction;
using AsyncAwaitBestPractices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

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
            if ((NeteasePlaylist)e.Parameter != null)
            {
                ViewModel.PlayList = (NeteasePlaylist?)e.Parameter;
                await ViewModel.GetSongsAsync();
            }
            SongsListView.ViewModel = ViewModel;
            Bindings.Update();
            SongsListView.Update();
        }

        private async void LikeBtnClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.SubscribePlaylistAsync();
            ViewModel.PlayList.Subscribed = !ViewModel.PlayList.Subscribed;
        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = ViewModel.SongsList.Where(t => t.Name.Contains(sender.Text)).Select(t => t.Name).ToList();
                sender.ItemsSource = suggestions;
            }
        }

        private async void AutoSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var song = ViewModel.SongsList.First(t => t.Name == args.SelectedItem.ToString());
            SongsListView.ScrollToIndex(ViewModel.SongsList.IndexOf(song));
        }

        private void SortButton_Click(SplitButton sender, SplitButtonClickEventArgs args)
        {
            ViewModel.ToggleSortOrder();
            var splitButton = (ToggleSplitButton)sender;
            var sortToolTip = (ToolTip)splitButton.FindName("SortToolTip");
            var fontIcon = (FontIcon)splitButton.FindName("SortIcon");

            switch (ViewModel.SortOrder)
            {
                case 1: // 升序
                    sortToolTip.Content="升序";
                    fontIcon.Glyph = "\uE74A";
                    splitButton.IsChecked = true;
                    break;
                case 2: // 降序
                    sortToolTip.Content = "降序";
                    fontIcon.Glyph = "\uE74B";
                    splitButton.IsChecked = true;
                    break;
                default: // 默认
                    sortToolTip.Content = "排序方式";
                    fontIcon.Glyph = "\uE8CB";
                    splitButton.IsChecked = false;
                    break;
            }
        }

        private void SortRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            ViewModel.SortTypes = (sender as RadioButton).Tag.ToString();
            ViewModel.SortSongListOrder();
        }
    }

    public class PlaylistPageBase : AppPageWithScopedViewModelBase<NeteasePlaylistViewModel>
    {
        
    }
}
