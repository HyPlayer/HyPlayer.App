using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


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
