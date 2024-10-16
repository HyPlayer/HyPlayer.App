using CommunityToolkit.WinUI.UI;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.Views.Controls.Netease.SongListView
{
    public sealed partial class SongListView : UserControl
    {
        public static readonly DependencyProperty ListSourceProperty = DependencyProperty.Register(
            "ListSource", typeof(string),
            typeof(SongListView),
            new PropertyMetadata(null)
        );

        public static readonly DependencyProperty ListHeaderProperty = DependencyProperty.Register(
        "ListHeader", typeof(UIElement), typeof(SongListView), new PropertyMetadata(default(UIElement)));

        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            "Footer", typeof(UIElement), typeof(SongListView), new PropertyMetadata(default(UIElement)));
        public NeteasePlaylistViewModel? ViewModel;
        public string ListSource
        {
            get => (string)GetValue(ListSourceProperty);
            set => SetValue(ListSourceProperty, value);
        }

        public UIElement ListHeader
        {
            get => (UIElement)GetValue(ListHeaderProperty);
            set
            {
                HeaderPanel.Padding = new Thickness(0, 0, 0, 25);
                SetValue(ListHeaderProperty, value);
            }
        }

        public UIElement Footer
        {
            get => (UIElement)GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public SongListView()
        {
            this.InitializeComponent();
        }


        public void Update()
        {
            this.Bindings.Update();
        }

        private void SongItemOnClick(object sender, ItemClickEventArgs e)
        {
            var song = (NeteaseSong)e.ClickedItem;
            Debug.WriteLine($"Clicking on {song.Name}");
            //TODO: Play the song
        }

        public async void ScrollToIndex(int index)
        {
            if (ViewModel?.SongsList != null)
            {
                SongContainer.SelectedItem = ViewModel.SongsList[index];
                await SongContainer.SmoothScrollIntoViewWithIndexAsync(index, ScrollItemPlacement.Top, false, true);
            }
        }
    }
}
