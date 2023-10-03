using Microsoft.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using WinUIEx;


namespace HyPlayer.Views.Window
{
    public partial class SmallWindow : WindowEx
    {
        public new Frame Content;
        public SmallWindow()
        {
            InitializeComponent();
            AppWindow.SetPresenter((Microsoft.UI.Windowing.AppWindowPresenterKind)AppWindowPresentationKind.CompactOverlay);
            
            
            SetTitleBar(AppTitleBar);

            Content = contentFrame;
        }
    }
}
