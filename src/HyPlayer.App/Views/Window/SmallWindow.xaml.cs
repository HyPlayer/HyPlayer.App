using Microsoft.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HyPlayer.Views.Window
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class SmallWindow : WindowEx
    {
        public new Frame Content;
        public SmallWindow()
        {
            this.InitializeComponent();
            this.AppWindow.SetPresenter((Microsoft.UI.Windowing.AppWindowPresenterKind)AppWindowPresentationKind.CompactOverlay);


            this.SetTitleBar(AppTitleBar);

            Content = contentFrame;
        }
    }
}
