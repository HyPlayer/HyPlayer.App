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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.App.Views.Controls.Netease.SongListView
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
    }
}
