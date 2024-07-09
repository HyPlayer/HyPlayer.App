using HyPlayer.Interfaces.Views;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Netease.SongListView;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.Views.Controls.SingleSongView
{
    public sealed partial class SingleSongView : SingleSongViewBase
    {
        public static readonly DependencyProperty SongProperty = DependencyProperty.Register(
    "DisplaySong", typeof(NeteaseSong),
    typeof(SingleSongView),
    new PropertyMetadata(null));
        public NeteaseSong DisplaySong
        {
            get => ((NeteaseSong)GetValue(SongProperty));
            set
            {
                SetValue(SongProperty, value);
            }
        }

        public SingleSongView()
        {
            this.InitializeComponent();
        }
        public void SetSong(NeteaseSong song, int loadIndex = -1)
        {
            ViewModel.NcmSong = song;
            ViewModel.GetSongInfo();
            ViewModel.LoadIndex=loadIndex; ;
            Bindings.Update();
        }
    }
    public class SingleSongViewBase : ReactiveControlBase<SongViewModel>
    {

    }
}
