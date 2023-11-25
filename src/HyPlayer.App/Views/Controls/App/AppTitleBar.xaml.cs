using HyPlayer.Interfaces.Views;
using HyPlayer.ViewModels;
using HyPlayer.Views.Controls.Search;
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


namespace HyPlayer.Views.Controls.App
{
    public sealed partial class AppTitleBar : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(AppTitleBar), new PropertyMetadata("Title"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register(
            nameof(Subtitle), typeof(string), typeof(AppTitleBar), new PropertyMetadata("Subtitle"));

        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(BitmapIcon), typeof(AppTitleBar), new PropertyMetadata(default(ImageSource)));

        public BitmapIcon Icon
        {
            get => (BitmapIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty SearchBoxProperty = DependencyProperty.Register(
            nameof(SearchBox), typeof(UserControl), typeof(AppTitleBar), new PropertyMetadata(null));

        public UserControl SearchBox
        {
            get => (UserControl)GetValue(SearchBoxProperty);
            set => SetValue(SearchBoxProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header), typeof(UIElement), typeof(AppTitleBar), new PropertyMetadata(null));

        public UIElement Header
        {
            get => (UIElement)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            nameof(Footer), typeof(UIElement), typeof(AppTitleBar), new PropertyMetadata(null));

        public UIElement Footer
        {
            get => (UIElement)GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }
        #endregion
        public AppTitleBar()
        {
            this.InitializeComponent();
        }
    }

    public class AppTitleBarBase : ReactiveControlBase<ShellViewModel>
    {
    }
}
