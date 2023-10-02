using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.Views.Controls.App
{
    public sealed class TopChartButton : Button
    {

        public static readonly DependencyProperty LeaderBoardNameProperty = DependencyProperty.Register(
            nameof(LeaderBoardName), typeof(string), typeof(TopChartButton), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty CoverProperty = DependencyProperty.Register(
            nameof(Cover), typeof(ImageSource), typeof(TopChartButton), new PropertyMetadata(default(ImageSource?)));

        public ImageSource Cover
        {
            get => (ImageSource)GetValue(CoverProperty);
            set => SetValue(CoverProperty, value);
        }

        public static readonly DependencyProperty PlayCountProperty = DependencyProperty.Register(
            nameof(PlayCount), typeof(string), typeof(TopChartButton), new PropertyMetadata(default(string)));

        public string PlayCount
        {
            get => (string)GetValue(PlayCountProperty);
            set => SetValue(PlayCountProperty, value);
        }
        
        public string LeaderBoardName
        {
            get => (string)GetValue(LeaderBoardNameProperty);
            set => SetValue(LeaderBoardNameProperty, value);
        }
        
        public TopChartButton()
        {
            this.DefaultStyleKey = typeof(TopChartButton);
        }
    }
}
