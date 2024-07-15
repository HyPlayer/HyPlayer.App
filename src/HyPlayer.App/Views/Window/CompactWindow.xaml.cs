using Microsoft.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using WinUIEx;


namespace HyPlayer.Views.Window
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class CompactWindow : WindowEx
    {
        public new Frame Content;
        public CompactWindow()
        {
            InitializeComponent();

            AppWindow.SetPresenter((Microsoft.UI.Windowing.AppWindowPresenterKind)AppWindowPresentationKind.CompactOverlay);
            SetTitleBar(AppTitleBar);

        }
    }
}
