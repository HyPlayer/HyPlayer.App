using CoolControls.WinUI3.Controls;
using HyPlayer.NeteaseProvider.Models;
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

namespace HyPlayer.App.Views.Controls.Netease.PlayListView
{
    public sealed partial class PlayListViewItem : UserControl
    {
        private static readonly DependencyProperty PlayListProperty =
            DependencyProperty.Register("PlayList", typeof(NeteasePlaylist), typeof(PlayListViewItem), new PropertyMetadata(default(NeteasePlaylist)));

        public NeteasePlaylist PlayList
        {
            get => (NeteasePlaylist)GetValue(PlayListProperty);
            set => SetValue(PlayListProperty, value);
        }

        public PlayListViewItem()
        {
            this.InitializeComponent();
        }
    }
}
